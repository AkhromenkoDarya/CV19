using System;
using System.ComponentModel;
using System.Windows;

namespace CV19.Views.Windows.Deanery
{
    public partial class StudentEditingWindow
    {
        #region FirstName : string - Имя студента

        /// <summary>
        /// Имя студента.
        /// </summary>
        public static readonly DependencyProperty FirstNameProperty
            = DependencyProperty.Register(
                nameof(FirstName),
                typeof(string),
                typeof(StudentEditingWindow),
                new PropertyMetadata(default(string)));

        /// <summary>
        /// Имя студента.
        /// </summary>
        [Description("Имя студента")]
        // [Category("")]
        public string FirstName
        {
            get => (string)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }

        #endregion

        #region LastName : string - Фамилия студента

        /// <summary>
        /// Фамилия студента.
        /// </summary>
        public static readonly DependencyProperty LastNameProperty
            = DependencyProperty.Register(
                nameof(LastName),
                typeof(string),
                typeof(StudentEditingWindow),
                new PropertyMetadata(default(string)));

        /// <summary>
        /// Фамилия студента.
        /// </summary>
        [Description("Фамилия студента")]
        // [Category("")]
        public string LastName
        {
            get => (string)GetValue(LastNameProperty);
            set => SetValue(LastNameProperty, value);
        }

        #endregion

        #region Patronymic : string - Отчество студента

        /// <summary>
        /// Отчество студента.
        /// </summary>
        public static readonly DependencyProperty PatronymicProperty
            = DependencyProperty.Register(
                nameof(Patronymic),
                typeof(string),
                typeof(StudentEditingWindow),
                new PropertyMetadata(default(string)));

        /// <summary>
        /// Отчество студента.
        /// </summary>
        [Description("Отчество студента")]
        // [Category("")]
        public string Patronymic
        {
            get => (string)GetValue(PatronymicProperty);
            set => SetValue(PatronymicProperty, value);
        }

        #endregion

        #region Rating : double - Оценка студента

        /// <summary>
        /// Оценка студента.
        /// </summary>
        public static readonly DependencyProperty RatingProperty
            = DependencyProperty.Register(
                nameof(Rating),
                typeof(double),
                typeof(StudentEditingWindow),
                new PropertyMetadata(default(double)));

        /// <summary>
        /// Оценка студента.
        /// </summary>
        [Description("Рейтинг студента")]
        // [Category("")]
        public double Rating
        {
            get => (double)GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }

        #endregion

        #region Birthday : DateTime - Дата рождения студента

        /// <summary>
        /// Дата рождения студента.
        /// </summary>
        public static readonly DependencyProperty BirthdayProperty
            = DependencyProperty.Register(
                nameof(Birthday),
                typeof(DateTime),
                typeof(StudentEditingWindow),
                new PropertyMetadata(default(DateTime)));

        /// <summary>
        /// Дата рождения студента.
        /// </summary>
        [Description("Дата рождения студента")]
        // [Category("")]
        public DateTime Birthday
        {
            get => (DateTime)GetValue(BirthdayProperty);
            set => SetValue(BirthdayProperty, value);
        }

        #endregion

        public StudentEditingWindow() => InitializeComponent();
    }
}
