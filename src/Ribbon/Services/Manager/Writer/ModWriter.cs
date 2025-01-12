using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Ribbon.Models;

namespace Ribbon.Services.Manager.Writer;

public class ModWriter(ModWriter.ModWriterOptions options)
{
    public class ModWriterOptions : INotifyPropertyChanged
    {
        private string _outputDirectory = Path.Combine(Environment.CurrentDirectory, OutputFileName);

        public string OutputDirectory
        {
            get => _outputDirectory;
            set => SetField(ref _outputDirectory, Path.Combine(value, OutputFileName));
        }
        
        public const string OutputFileName = "mods.json";
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public void Write(Dictionary<int, DetailedModFile>? data, NotifyCollectionChangedEventArgs e)
    {
        var content = JsonSerializer.Serialize(data);
        File.WriteAllText(options.OutputDirectory, content);
    }
}