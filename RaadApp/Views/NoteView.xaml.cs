namespace RaadApp.Views;

public partial class NoteView : ContentView
{
	public NoteView()
	{
		InitializeComponent();
		BindingContext =new ViewMadles.NoteViewMadle();

    }
}