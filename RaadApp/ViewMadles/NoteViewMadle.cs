using RaadApp.Madels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace RaadApp.ViewMadles
{
    public partial class NoteViewMadle : INotifyPropertyChanged
    {
        //fields
        private string _noteTitle;
        private string _noteDescription;
        private Note _SelectedNote;
        //get and set
        public string NoteTitle
        {
            get => _noteTitle;
           set
            {
                if (_noteTitle != value)
                {
                    _noteTitle = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NoteDescription
        {
            get => _noteDescription;
            set
            {
                if (_noteDescription != value)
                {
                    _noteDescription = value;
                    OnPropertyChanged();



                }
            }
        }
        
        public Note SelectedNote
        {
            get => _SelectedNote;
            set
            {
                if (_SelectedNote != value)
                {
                    _SelectedNote = value;
                    NoteTitle=_SelectedNote.Title;
                    NoteDescription=_SelectedNote.Description;  
                    OnPropertyChanged();


                }
            }
        }

        //property

        public ObservableCollection<Note> NoteCollection {  get; set; }
        public ICommand AddNoteCommand { get;  }
        public ICommand EditeNoteCommand { get;  }
        public ICommand RemoveNoteCommand { get;  }

        public NoteViewMadle()
        {
            NoteCollection = new ObservableCollection<Note>();
            AddNoteCommand = new Command(AddNote);
            RemoveNoteCommand = new Command(DeleteNote);
            EditeNoteCommand = new Command(EditeNote);
        }

        private void DeleteNote(object obj)
        {
            if (SelectedNote != null)
            {
                NoteCollection.Remove(SelectedNote);
                //Rest Valuse
                NoteTitle = string.Empty;
                NoteDescription = string.Empty;
            }
        } private void EditeNote(object obj)
        {
            if (SelectedNote != null)
            {
                foreach (Note note in NoteCollection.ToList())
                {
                    if (note == SelectedNote)
                    {
                        var newNote = new Note
                        {
                            Id = note.Id,
                            Title = NoteTitle,
                            Description = NoteDescription,
                        };
                        //Remove Note
                        NoteCollection.Remove(note);
                        NoteCollection.Add(newNote);    
                    }
        
                }
            }
        }
        private void AddNote(object obj)
        {   // Genrate a unique  ID  for the new person 
            int newId = NoteCollection.Count > 0 ? NoteCollection.Max(p => p.Id) + 1 : 1;
            // set new note
            var note = new Note
            {
                Id = newId,
                Title = _noteTitle,
                Description = _noteDescription,
            };
            NoteCollection.Add(note);
            //Rest Valuse
            NoteTitle = string.Empty;
            NoteDescription = string.Empty;
        }



        //property Changed
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
