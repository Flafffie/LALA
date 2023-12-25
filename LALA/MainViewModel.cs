using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace LALA
{
    public class MainViewModel : ReactiveObject
    {
        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students
        {
            get => _students;
            set => this.RaiseAndSetIfChanged(ref _students, value);
        }

        private string _newStudentName;
        public string NewStudentName
        {
            get => _newStudentName;
            set => this.RaiseAndSetIfChanged(ref _newStudentName, value);
        }

        private double _newStudentGrade;
        public double NewStudentGrade
        {
            get => _newStudentGrade;
            set => this.RaiseAndSetIfChanged(ref _newStudentGrade, value);
        }

        public ReactiveCommand<Unit, Unit> AddStudentCommand { get; }

        public MainViewModel()
        {
            Students = new ObservableCollection<Student>();

            AddStudentCommand = ReactiveCommand.Create(AddStudent);
        }

        private void AddStudent()
        {
            var newStudent = new Student
            {
                Name = NewStudentName,
                Grade = NewStudentGrade
            };

            using (var context = new StudentDbContext())
            {
                context.Students.Add(newStudent);
                context.SaveChanges();
            }

            Students.Add(newStudent);

            // Очистка полей после добавления
            NewStudentName = string.Empty;
            NewStudentGrade = 0.0;
        }
    }

}
