using CV19.Models.Deanery;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CV19
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Group group))
            {
                return;
            }

            if (group.Name is null)
            {
                return;
            }

            string filterText = GroupNameFilterTextBox.Text;

            if (string.IsNullOrWhiteSpace(filterText))
            {
                return;
            }

            if (group.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (group.Description != null && group.Description.Contains(filterText, 
                StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            e.Accepted = false;
        }

        private void GroupNameFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var collection = (CollectionViewSource)textBox.FindResource("GroupCollection");
            collection.View.Refresh();
        }
    }
}
