using System;
using System.ComponentModel;
using System.Windows;

namespace CV19.Views.Windows.Deanery
{
    public partial class StudentEditingWindow
    {
        #region Name : string - Имя студента

        /// <summary>
        /// Имя студента.
        /// </summary>
        public static readonly DependencyProperty NameProperty
            = DependencyProperty.Register(
                nameof(Name),
                typeof(string),
                typeof(StudentEditingWindow),
                new PropertyMetadata(default(string)));

        /// <summary>
        /// Имя студента.
        /// </summary>
        [Description("Имя студента")]
        // [Category("")]
        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        #endregion

        #region Surname : string - Фамилия студента

        /// <summary>
        /// Фамилия студента.
        /// </summary>
        public static readonly DependencyProperty SurnameProperty
            = DependencyProperty.Register(
                nameof(Surname),
                typeof(string),
                typeof(StudentEditingWindow),
                new PropertyMetadata(default(string)));

        /// <summary>
        /// Фамилия студента.
        /// </summary>
        [Description("Фамилия студента")]
        // [Category("")]
        public string Surname
        {
            get => (string)GetValue(SurnameProperty);
            set => SetValue(SurnameProperty, value);
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

        #region Rating : double - Рейтинг студента

        /// <summary>
        /// Рейтинг студента.
        /// </summary>
        public static readonly DependencyProperty RatingProperty
            = DependencyProperty.Register(
                nameof(Rating),
                typeof(double),
                typeof(StudentEditingWindow),
                new PropertyMetadata(default(double)));

        /// <summary>
        /// Рейтинг студента.
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
