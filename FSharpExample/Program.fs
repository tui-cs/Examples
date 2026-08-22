// A simple Terminal.Gui example in F#.
// For the full range of functionality see the UICatalog project in the Terminal.Gui repo.

open System
open System.Threading
open Terminal.Gui.App
open Terminal.Gui.Input
open Terminal.Gui.ViewBase
open Terminal.Gui.Views

// Defines a top-level window with border and title
type ExampleWindow () as this =
    inherit Window ()

    do
        this.Title <- sprintf "Example App (%O to quit)" (Application.GetDefaultKey (Command.Quit))

        // Create input components and labels
        let usernameLabel = new Label (Text = "Username:")

        let userNameText =
            new TextField (X = Pos.Right (usernameLabel) + Pos.op_Implicit (1), Width = Dim.Fill ())

        let passwordLabel =
            new Label (Text = "Password:", X = Pos.Left (usernameLabel), Y = Pos.Bottom (usernameLabel) + Pos.op_Implicit (1))

        let passwordText =
            new TextField (Secret = true, X = Pos.Left (userNameText), Y = Pos.Top (passwordLabel), Width = Dim.Fill ())

        // Create login button
        let btnLogin =
            new Button (Text = "Login", Y = Pos.Bottom (passwordLabel) + Pos.op_Implicit (1), X = Pos.Center (), IsDefault = true)

        // When login button is clicked display a message popup
        btnLogin.Accepting.Add (fun e ->
            match this.App with
            | null -> ()
            | app ->
                if userNameText.Text = "admin" && passwordText.Text = "password" then
                    MessageBox.Query (app, "Logging In", "Login Successful", "Ok") |> ignore
                    ExampleWindow.UserName <- userNameText.Text
                    app.RequestStop ()
                else
                    MessageBox.ErrorQuery (app, "Logging In", "Incorrect username or password", "Ok") |> ignore

            // When Accepting is handled, set e.Handled to true to prevent further processing.
            e.Handled <- true)

        // Add the views to the Window
        this.Add (usernameLabel, userNameText, passwordLabel, passwordText, btnLogin)

    static member val UserName = "" with get, set

[<EntryPoint>]
let main argv =
    // Configuration (themes, schemes, settings) is applied automatically at assembly load.
    let app = Application.Create().Init ()

    let smokeTest = argv.Length > 0 && argv.[0] = "--smoke-test"

    if smokeTest then
        // Start, render, and exit cleanly after 2 seconds (used by tests/Examples.SmokeTests).
        use cts = new CancellationTokenSource (TimeSpan.FromSeconds 2.0)
        app.RunAsync<ExampleWindow>(cts.Token).GetAwaiter().GetResult () |> ignore
        app.Dispose ()
        printfn "Smoke test passed."
        0
    else
        app.Run<ExampleWindow> () |> ignore

        // Dispose the application to free resources and restore the previous screen
        app.Dispose ()

        // To see this output on the screen it must be done after Dispose,
        // which restores the previous screen.
        printfn "Username: %s" ExampleWindow.UserName

        0 // return an integer exit code
