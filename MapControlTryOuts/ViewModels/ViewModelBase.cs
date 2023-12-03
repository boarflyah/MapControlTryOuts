using CoreClassesLib.AbstractsAndInterfaces;
using CoreClassesLib.Contexts;
using MapControlTryOuts.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControlTryOuts.ViewModels
{
    public abstract class ViewModelBase: BaseNotifyPropertyChanged
    {
        protected readonly IPointsContext _Context;

        protected ViewModelBase(IPointsContext context)
        {
            this._Context = context;
        }
    }
}
