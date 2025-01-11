using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using CurseForge.APIClient.Models.Mods;
using Ribbon.Services.Manager.Writer;

namespace Ribbon.Services.State;

public sealed class StateOptions : INotifyPropertyChanged
{
    private ModLoaderType _modLoaderType;
    
    public ModLoaderType ModLoaderType
    {
        get => _modLoaderType;
        set => SetField(ref _modLoaderType, value);
    }

    private string _gameVersion;
    
    public string GameVersion 
    { 
        get => _gameVersion; 
        set => SetField(ref _gameVersion, value); 
    }
    
    public ModWriter.ModWriterOptions ModWriterOptions { get; set; } = new();
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"SETTINGS:");
        sb.AppendLine($"Game version: {GameVersion}");
        sb.AppendLine($"Mod loader type: {ModLoaderType}");
        return sb.ToString();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}