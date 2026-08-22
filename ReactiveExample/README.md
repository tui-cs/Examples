This is a sample app that shows how to use `System.Reactive` and `ReactiveUI` with `Terminal.Gui`. The app uses the MVVM architecture that may seem familiar to folks coming from WPF, Xamarin Forms, UWP, Avalonia, or Windows Forms. In this app, we implement the data bindings using ReactiveUI `WhenAnyValue` syntax and [ObservableEvents](https://github.com/reactivemarbles/ObservableEvents) — a Source Generator that turns events into observable wrappers.

<img src="https://user-images.githubusercontent.com/6759207/94748621-646a7280-038a-11eb-8ea0-34629dc799b3.gif" width="450">

### Scheduling

In order to use reactive extensions scheduling, copy-paste the `TerminalScheduler.cs` file into your project, and add the following lines to the composition root of your `Terminal.Gui` application:

```cs
// Configuration (themes, schemes, settings) is applied automatically at assembly load.
using IApplication app = Application.Create ();
app.Init ();
RxApp.MainThreadScheduler = new TerminalScheduler (app);
RxApp.TaskpoolScheduler = TaskPoolScheduler.Default;

LoginView loginView = new (new ());
app.Run (loginView);
loginView.Dispose ();
```

From now on, you can use `.ObserveOn(RxApp.MainThreadScheduler)` to return to the main loop from a background thread. This is useful when you have a `IObservable<TValue>` updated from a background thread, and you wish to update the UI with `TValue`s received from that observable.

### Data Bindings

If you wish to implement `OneWay` data binding, then use the `WhenAnyValue` [ReactiveUI extension method](https://www.reactiveui.net/docs/handbook/when-any/) that listens to `INotifyPropertyChanged` events of the specified property, and converts that events into `IObservable<TProperty>`:

```cs
// 'usernameInput' is 'TextField'
ViewModel
    .WhenAnyValue (x => x.Username)
    .BindTo (usernameInput, x => x.Text);
```

Note that your view model should implement `INotifyPropertyChanged` or inherit from a `ReactiveObject`. If you wish to implement `OneWayToSource` data binding, listen to the view's change event via `Observable.FromEventPattern`. For `TextField` specifically, the generated `.Events ()` wrappers do **not** work — as of Terminal.Gui 2.5 the ObservableEvents source generator cannot wrap `TextField`, which hides `View.TextChanging` with a different delegate type (`.Events ()` remains fine for other views, like the `Button` below). `TextField` implements `IValue<string>`, so its `ValueChanged` event delivers the new value directly and only fires on real changes:

```cs
// 'usernameInput' is 'TextField'
Observable
    .FromEventPattern<ValueChangedEventArgs<string?>> (
        h => usernameInput.ValueChanged += h,
        h => usernameInput.ValueChanged -= h)
    .Select (e => e.EventArgs.NewValue ?? string.Empty)
    .BindTo (ViewModel, x => x.Username);
```

If you combine `OneWay` and `OneWayToSource` data bindings, you get `TwoWay` data binding. Invoking commands should be as simple as this:

```cs
// 'clearButton' is 'Button'
clearButton
    .Events ()
    .Accepting
    .InvokeCommand (ViewModel, x => x.Clear);
```
