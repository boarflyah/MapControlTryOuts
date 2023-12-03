using CoreClassesLib.AbstractsAndInterfaces;
using CoreClassesLib.Models.Points;
using CoreClassesLib.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Events
{
    public class Command: BaseNotifyPropertyChanged
    {
        public int CommandId { get; set; }

        private DateTime? _CommandDate;
		public DateTime? CommandDate
		{
			get => _CommandDate;
			set
			{
				if (_CommandDate != value)
				{
					_CommandDate = value;
					RaisePropertyChanged(nameof(CommandDate));
				}
			}
		}

        private DateTime? _DueDate;
        public DateTime? DueDate
        {
            get => _DueDate;
            set
            {
                if (_DueDate != value)
                {
                    _DueDate = value;
                    RaisePropertyChanged(nameof(DueDate));
                }
            }
        }

        private DateTime? _ExecutionDate;
        public DateTime? ExecutionDate
        {
            get => _ExecutionDate;
            set
            {
                if (_ExecutionDate != value)
                {
                    _ExecutionDate = value;
                    RaisePropertyChanged(nameof(ExecutionDate));
                }
            }
        }

        private ResourceObject _Executor;
        public ResourceObject Executor
        {
            get => _Executor; 
            set 
            {
                if (value != _Executor)
                {
                    _Executor = value;
                    RaisePropertyChanged(nameof(Executor));
                }
            }
        }

        private ResourceObject _TargetResource;
        public ResourceObject TargetResource
        {
            get => _TargetResource;
            set
            {
                if (value != _TargetResource)
                {
                    _TargetResource = value;
                    RaisePropertyChanged(nameof(TargetResource));
                }
            }
        }

        private Command _RelatedCommand;
        public Command RelatedCommand
        {
            get => _RelatedCommand; 
            set 
            {
                if (value != _RelatedCommand)
                {
                    _RelatedCommand = value;
                    RaisePropertyChanged(nameof(RelatedCommand));
                }
            }
        }


        private string _CommandText;
        public string CommandText
        {
            get => _CommandText;
            set 
            {
                if(value != _CommandText)
                {
                    _CommandText = value;
                    RaisePropertyChanged(nameof(CommandText));
                }
            }
        }
    }
}
