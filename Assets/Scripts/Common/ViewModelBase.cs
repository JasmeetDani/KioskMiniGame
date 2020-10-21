using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public abstract class ViewModelBase<VM, C> : MonoBehaviour, INotifyPropertyChanged
                                                    where VM : ViewModelBase<VM, C>
                                                    where C : ControllerBase<C, VM>
{
    [Inject]
    protected C controller;
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    void OnEnable()
    {
        controller.OnEnable();
    }
    void OnDisable()
    {
        controller.OnDisable();
    }
    
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}