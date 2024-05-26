using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Avalonia.Media.Imaging;
using DemoExamTraining2.Helpers;
using DemoExamTraining2.Models;

namespace DemoExamTraining2.ViewModels;

public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    private static Bitmap _placeholder = ImageHelper.LoadFromResource(new Uri("avares://DemoExamTraining2/Assets/Images/placeholder.jpg"));
    private string _keywords;
    private bool _isSelected;
    private int _filterIdx = -1;
    private int _sortingIdx = -1;
    private static ObservableCollection<Agent> _selected = new ();
    private List<string> _agentTypes = new List<string>()
    {
        "Все типы",
        "Lv1_Crook",
        "Lv2_Agent",
        "Lv3_Agent",
        "Lv4_Agent"
    };
    private List<AgentType> _agentTypesForRendering = new List<AgentType>(){new AgentType("All types")}.Concat(Context.AgentTypes.Select(el => el)).ToList();
    private static ObservableCollection<Agent> _agents = new()
    {
        new Agent("Михалыч", "89111234567", "bebrik@bk.ru", "сам себе начальник", "111222333444", "ул. Кузи Лакомкина 1", 4, Placeholder, Context.AgentTypes[0]),
        new Agent("Мамадзиё Нагзибеков", "89914234567", "bebrik@bk.ru", "Анатолий", "3235791850392", "ул. Кузи Лакомкина 2", 3, Placeholder, Context.AgentTypes[1]),
        new Agent("Агент", "89914333333", "agentk@gmail.com", "Анатолий", "3235791834662", "ул. Кузи Лакомкина 3/1", 2, Placeholder, Context.AgentTypes[2]),
        new Agent("Агент", "89914333333", "agent@gmail.com", "Анатолий", "0005791834662", "ул. Кузи Лакомкина 3/2", 5, Placeholder, Context.AgentTypes[3])
    };
    
    public MainWindowViewModel()
    {
        FilteredAgents = _agents;
    }
    public ObservableCollection<Agent> FilteredAgents { get; set; }

    public ObservableCollection<Agent> SelectedAgents
    {
        get => _selected;
        set
        {
            _selected = value;
            Console.WriteLine(_selected.Count);
            if (_selected.Count == 0)
            {
                IsSelected = false;
            }
            else
            {
                Console.WriteLine("did");
                IsSelected = true;
            }
            OnPropChanged(nameof(SelectedAgents));
        }
    }

    public ObservableCollection<Agent> Agents
    {
        get => _agents;
        set => _agents = value;
    }

    public static Bitmap Placeholder
    {
        get => _placeholder;
    }

    public List<AgentType> AgentTypesForRender
    {
        get => _agentTypesForRendering;
    }

    public bool IsSelected
    {
        get => _isSelected;
        set => _isSelected = value;
    }

    public int FilterIdx
    {
        get => _filterIdx;
        set
        {
            _filterIdx = value;
            FilterAgents();
        }
    }
    
    public int SortingIdx
    {
        get => _sortingIdx;
        set => _sortingIdx = value;
    }

    public List<string> AgentTypes
    {
        get => _agentTypes;
    }
    public string Keywords
    {
        get => _keywords;
        set
        {
            _keywords = value;
            SearchAgents();
        }
    }

    private void FilterAgents()
    {
        if (_filterIdx == 0)
        {
            FilteredAgents = _agents;
        }
        else
        {
            FilteredAgents = new ObservableCollection<Agent>(_agents.Where(
                agent => (agent.Type.Name == _agentTypes[_filterIdx])));
        }
        OnPropChanged(nameof(FilteredAgents));
    }

    private void SearchAgents()
    {
        if (_keywords == "")
        {
            FilteredAgents = _agents;
        }
        else
        {
            FilteredAgents = new ObservableCollection<Agent>(_agents.Where(
                agent => (agent.Name.ToLower().Contains(_keywords.ToLower()) 
                          || agent.Email.ToLower().Contains(_keywords.ToLower()) 
                          || agent.PhoneNum.Contains(_keywords)))
            );
        }
        OnPropChanged(nameof(FilteredAgents));
    }

    public void ClearFilters()
    {
        FilteredAgents = new ObservableCollection<Agent>(FilteredAgents.OrderBy(agent => agent.Id));
        OnPropChanged(nameof(FilteredAgents));
    }

    public void SortAgentsByNameAsc()
    {
        FilteredAgents = new ObservableCollection<Agent>(FilteredAgents.OrderBy(agent => agent.Name));
        OnPropChanged(nameof(FilteredAgents));
    }
    
    public void SortAgentsByNameDesc()
    {
        FilteredAgents = new ObservableCollection<Agent>(FilteredAgents.OrderByDescending(agent => agent.Name));
        OnPropChanged(nameof(FilteredAgents));
    }
    
    public void SortAgentsByDiscountAsc()
    {
        FilteredAgents = new ObservableCollection<Agent>(FilteredAgents.OrderBy(agent => agent.Discount));
        OnPropChanged(nameof(FilteredAgents));
    }
    
    public void SortAgentsByDiscountDesc()
    {
        FilteredAgents = new ObservableCollection<Agent>(FilteredAgents.OrderByDescending(agent => agent.Discount));
        OnPropChanged(nameof(FilteredAgents));
    }
    
    public void SortAgentsByPriorityAsc()
    {
        FilteredAgents = new ObservableCollection<Agent>(FilteredAgents.OrderBy(agent => agent.Priority));
        OnPropChanged(nameof(FilteredAgents));
    }
    
    public void SortAgentsByPriorityDesc()
    {
        FilteredAgents = new ObservableCollection<Agent>(FilteredAgents.OrderByDescending(agent => agent.Priority));
        OnPropChanged(nameof(FilteredAgents));
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropChanged(string propName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
    
}