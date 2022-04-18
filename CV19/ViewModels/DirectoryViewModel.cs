using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CV19.ViewModels
{
    internal class DirectoryViewModel : ViewModel
    {
        private readonly DirectoryInfo _directoryInfo;

        public IEnumerable<DirectoryViewModel> Subdirectories => _directoryInfo
            .EnumerateDirectories()
            .Select(directory => new DirectoryViewModel(directory.FullName));

        public IEnumerable<FileViewModel> Files => _directoryInfo
            .EnumerateFiles()
            .Select(file => new FileViewModel(file.FullName));

        public IEnumerable<object> DirectoryItems => Subdirectories.Cast<object>().Concat(Files);

        public string Name => _directoryInfo.Name;

        public string Path => _directoryInfo.FullName;

        public DateTime CreationTime => _directoryInfo.CreationTime;

        public DirectoryViewModel(string path) => _directoryInfo = new DirectoryInfo(path);
    }
}
