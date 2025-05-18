using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Taxi.Desktop.Models;

namespace Taxi.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly TaxiDbContext _dbContext;
    
    /*private int? _id;
    public int? Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }*/
    [Reactive] public int? PersonId { get; set; }
    [Reactive] public string? LastName { get; set; }
    [Reactive] public string? FirstName { get; set; }

    public ObservableCollection<Person> Drivers { get; set; } = [];
    [Reactive] public Person? SelectedDriver { get; set; }
    
    public ReactiveCommand<Unit, Unit>? CommandSaveDriver { get; }
    public ReactiveCommand<Unit, Unit>? CommandClear { get; }

    public MainWindowViewModel()
    {
        this.WhenAnyValue(vm => vm.SelectedDriver)
            .Subscribe(d =>
            {
                PersonId = d?.Id;
                FirstName = d?.FirstName;
                LastName = d?.LastName;
            });
        
        _dbContext = TaxiDbContextFactory.CreateDbContext();

        LoadDrivers();

        CommandSaveDriver = ReactiveCommand.Create(SaveDriver);
        CommandClear = ReactiveCommand.Create(Clear);
    }

    private void LoadDrivers()
    {
        Drivers.Clear();

        foreach (var driver in _dbContext.Drivers)
        {
            Drivers.Add(driver);
        }
    }

    public void SaveDriver()
    {
        if (PersonId.HasValue)
        {
            var driver = _dbContext.Drivers.Single(d => d.Id == PersonId);
            driver.FirstName = FirstName;
            driver.LastName = LastName;
        }
        else
        {
            _dbContext.Drivers.Add(new Person
            {
                FirstName = FirstName, 
                LastName = LastName
            });
        }
        
        _dbContext.SaveChanges();
        LoadDrivers();
        Clear();
    }

    private void Clear()
    {
        PersonId = null;
        FirstName = null;
        LastName = null;
        SelectedDriver = null;
    }
}