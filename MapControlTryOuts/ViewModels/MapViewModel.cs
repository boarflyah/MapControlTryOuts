using MapControlTryOuts.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CoreClassesLib.Contexts;
using MapControl;
using CoreClassesLib.Models.Points;
using CoreClassesLib.Models.Resources;
using CoreClassesLib.Models.Resources.Moving;

namespace MapControlTryOuts.ViewModels
{
    public class MapViewModel : ViewModelBase
    {


        public MapViewModel(IPointsContext _context) : base(_context)
        {
            //using (var context = new PointsContext())
            //{
            Resources = new ObservableCollection<ResourceObject>(_Context.Resources
                .Include(nameof(ResourceObject.TechnicalData))
                .Include(nameof(ResourceObject.Point))
                .Include(nameof(ResourceObject.Commands))
                .Where(x => x is BaseAmmoModel == false));

            HostileObjects = new ObservableCollection<HostileObjectModel>(_Context.Hostiles
                .Include(nameof(HostileObjectModel.Point)).
                Where(x => x.Point.Visible));

            AmmoContainers = new ObservableCollection<BaseAmmoModel>(_Context.Ammunition
                .Include(nameof(BaseAmmoModel.TechnicalData))
                .Include(nameof(BaseAmmoModel.Point)));

            //VisibleAmmo = CollectionViewSource.GetDefaultView(AmmoContainers.Where(x => x.Visible));
            //VisibleAmmo.Refresh();
            //}

            HitVisibility = false;

            Rectangle = new()
            {
                X = 0,
                Y = 0,
                Width = 0,
                Height = 0,
                IsMouseClickHold = false,
                Visible = false
            };

            HostilesMultiSelection = false;

            FireCommand = new(OnExecuteFireCommand, CanExecuteFireCommand);
            HostilesTrackingCommand = new(OnExecuteHostilesTracking, CanExecuteHostilesTracking);


            //Thread T = new Thread(RefreshPoints);
            //T.Start();

        }

        private void SelectedHostile_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //FireCommand.RaiseCanExecuteChanged();
            if (e.PropertyName == nameof(MovingResourceObject.Point.Location))
                DistanceBetweenSelected = SelectedResource != null && SelectedHostile != null ? SelectedResource.Point.Distance(SelectedHostile.Point) : 0;
        }

        private void SelectedResource_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //FireCommand.RaiseCanExecuteChanged();
            if (e.PropertyName == nameof(MovingResourceObject.Point.Location))
                DistanceBetweenSelected = SelectedResource != null && SelectedHostile != null ? SelectedResource.Point.Distance(SelectedHostile.Point) : 0;
        }

        protected override void OnPropertyChanged(string property, object oldValue = null, object newValue = null)
        {
            if (property == nameof(SelectedResource))
            {
                if (SelectedResource != null && SelectedResource.TechnicalData.MultiTargetHandling)
                    HostilesMultiSelection = true;
                else
                    HostilesMultiSelection = false;
                foreach (var v in HostileObjects.Where(x => x.Point.IsSelected))
                    v.Point.IsSelected = false;
            }
        }


        #region Collections

        public ObservableCollection<ResourceObject> Resources { get; set; }
        public ObservableCollection<HostileObjectModel> HostileObjects { get; set; }
        public ObservableCollection<BaseAmmoModel> AmmoContainers { get; set; }

        #endregion

        #region Properties
        public RectangleModel Rectangle { get; set; }

        private ResourceObject _SelectedResource;
        public ResourceObject SelectedResource
        {
            get
            {
                return _SelectedResource;
            }
            set
            {
                _SelectedResource = value;
                if (value != null)
                {
                    SelectedResource.PropertyChanged += SelectedResource_PropertyChanged;
                    FireCommand.RaiseCanExecuteChanged();
                    HostilesTrackingCommand.RaiseCanExecuteChanged();
                    DistanceBetweenSelected = SelectedResource != null && SelectedHostile != null ? SelectedResource.Point.Distance(SelectedHostile.Point) : 0;
                    RaisePropertyChanged(nameof(SelectedResource));
                }
            }
        }


        private HostileObjectModel _SelectedHostile;
        public HostileObjectModel SelectedHostile
        {
            get
            {
                return _SelectedHostile;
            }
            set
            {
                _SelectedHostile = value;
                if (value != null)
                {
                    RaisePropertyChanged(nameof(SelectedHostile));
                    SelectedHostile.PropertyChanged += SelectedHostile_PropertyChanged;
                    FireCommand.RaiseCanExecuteChanged();
                    DistanceBetweenSelected = SelectedResource != null && SelectedHostile != null ? SelectedResource.Point.Distance(SelectedHostile.Point) : 0;
                }
            }
        }

        private double _DistanceBetweenSelected;
        public double DistanceBetweenSelected
        {
            get => _DistanceBetweenSelected;
            set
            {
                if (_DistanceBetweenSelected != value)
                {
                    _DistanceBetweenSelected = value;
                    RaisePropertyChanged(nameof(DistanceBetweenSelected));
                }
            }
        }

        private bool _HostilesMultiSelection;

        public bool HostilesMultiSelection
        {
            get => _HostilesMultiSelection;
            set
            {
                if (_HostilesMultiSelection != value)
                {
                    _HostilesMultiSelection = value;
                    RaisePropertyChanged(nameof(HostilesMultiSelection));
                }
            }
        }

        private bool _HitVisibility;

        public bool HitVisibility
        {
            get => _HitVisibility;
            set
            {
                if (_HitVisibility != value)
                {
                    _HitVisibility = value;
                    RaisePropertyChanged(nameof(HitVisibility));
                }
            }
        }

        private Location _HitLocation;

        public Location HitLocation
        {
            get => _HitLocation;
            set
            {
                if (_HitLocation != value)
                {
                    _HitLocation = value;
                    RaisePropertyChanged(nameof(HitLocation));
                }
            }
        }

        private bool _IsPopupOpened;

        public bool IsPopupOpened
        {
            get => _IsPopupOpened;
            set
            {
                if (_IsPopupOpened != value)
                {
                    _IsPopupOpened = value;
                    RaisePropertyChanged(nameof(IsPopupOpened));
                }
            }
        }


        #endregion

        #region Commands
        public CommandBase FireCommand { get; set; }

        private bool CanExecuteFireCommand()
        {
            if (SelectedResource != null && SelectedResource.TechnicalData.IsHostileNeeded)
                return SelectedHostile != null && SelectedResource.AmmoContainer.Count > 0;
            return SelectedResource != null && SelectedResource.AmmoContainer.Count > 0;
        }

        private void OnExecuteFireCommand()
        {
            //AmmoFired();
            Thread T = new Thread(AmmoFired);
            T.Start();
            _Context.CommitChanges();
        }

        public CommandBase HostilesTrackingCommand { get; set; }

        private bool CanExecuteHostilesTracking()
        {
            return SelectedResource != null && SelectedResource is RadarSystem rs && rs.HostilesTrackingInProgress == false;
        }
        private void OnExecuteHostilesTracking()
        {
            //Command command;
            if (HostileObjects.Any(x => x.Point.IsSelected))
            {
                Thread T = new Thread(TrackHostiles);
                T.Start();
                //command = new Command()
                //{
                //    ExecutionDate = DateTime.Now,
                //    Executor = SelectedResource,
                //    CommandText = "Komenda: śledzenie celu"
                //};
            }
            else
            {
                DetectHostiles();
                //command = new Command()
                //{
                //    ExecutionDate = DateTime.Now,
                //    Executor = SelectedResource,
                //    CommandText = "Komenda: wykrywanie"
                //};
            }
            //_Context.Commands.Add(command);
            //_Context.CommitChanges();
            _Context.CommitChanges();
        }



        #endregion

        #region UI Reloads

        //private void ReloadLocations(List<BasePointModel> newLocations)
        //{
        //    if (App.Current.Dispatcher.HasShutdownStarted == false)
        //    {
        //        App.Current.Dispatcher.Invoke((Action)delegate
        //        {
        //            foreach (BasePointModel p in newLocations)
        //            {
        //                HostileObjects.FirstOrDefault(x => x.Point.BasePointModelId == p.BasePointModelId).Point.Visible = true;
        //                HostileObjects.FirstOrDefault(x => x.Point.BasePointModelId == p.BasePointModelId).Point.Location = new Location(p.Location.Latitude, p.Location.Longitude);
        //            }
        //        });
        //    }
        //}

        private void RemoveAndReloadAmmoFromContainer(BaseAmmoModel ammo, ExplosionPointModel explosion, bool removeAmmo = true)
        {
            if (App.Current.Dispatcher.HasShutdownStarted == false)
            {
                var ammoEntity = _Context.Ammunition.Find(ammo.BaseObjectId);
                bool toRemove = false;
                App.Current.Dispatcher.Invoke(() =>
                {
                    if (SelectedHostile != null)
                    {
                        //var ammoEntity = _Context.Ammunition.Find(ammo.BaseObjectId);
                        SelectedResource.AmmoContainer.Remove(ammo);
                        AmmoContainers.Remove(ammo);
                        //var resourceEntity = _Context.Resources.Find(SelectedResource.BaseObjectId);
                        toRemove = true;

                        SelectedHostile.Health -= explosion != null ? explosion.HealthTaken : 0;
                        if (explosion != null && explosion.IsTargetDestroyed)
                        {
                            HostileObjects.Remove(SelectedHostile);
                            SelectedHostile = null;
                            DistanceBetweenSelected = 0;
                            ShowHit(explosion);
                        }
                    }
                    else
                    {
                        ammo.Resource = null;
                        ammoEntity.Resource = null;
                        SelectedResource.AmmoContainer.Remove(ammo);
                        //var entity = _Context.Ammunition.Find(ammo.BaseObjectId);
                    }

                    FireCommand.RaiseCanExecuteChanged();
                });
                if (toRemove)
                    _Context.Ammunition.Remove(ammoEntity);
                _Context.CommitChanges();
            }
        }

        private void ShowHit(ExplosionPointModel explosion)
        {
            Task.Run(() =>
            {
                HitLocation = explosion.Point.Location;
                HitVisibility = true;
                Thread.Sleep(1000);
            }).ContinueWith(e => HitVisibility = false);
        }

        private void SetHostilesTrackingInProgress(RadarSystem rs, bool v)
        {
            if (App.Current.Dispatcher.HasShutdownStarted == false)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    rs.HostilesTrackingInProgress = v;
                    HostilesTrackingCommand.RaiseCanExecuteChanged();
                });
            }
        }

        #endregion

        #region Command Methods

        void AmmoFired()
        {
            List<BaseAmmoModel> fired = SelectedResource.ChooseAmmo(new object[] { Rectangle });
            foreach (BaseAmmoModel ammo in fired)
            {
                ExplosionPointModel tempExp = SelectedResource.TechnicalData.IsHostileNeeded ? SelectedResource.FireCommandExecute(SelectedHostile, ammo) : SelectedResource.FireCommandExecute(ammo);
                RemoveAndReloadAmmoFromContainer(ammo, tempExp);
            }

            //Command command = new Command()
            //{
            //    ExecutionDate = DateTime.Now,
            //    Executor = SelectedResource,
            //    CommandText = "Komenda: otworzyć ogień"
            //};

            //_Context.CommitChanges();
        }

        void DetectHostiles()
        {
            RadarSystem selected = SelectedResource as RadarSystem;
            List<HostileObjectModel> toRemove = new();
            foreach (var ho in HostileObjects)
            {
                if (ho.Point.Distance(selected.Point) < selected.RangedResourceTechnicalData.RangeRadius)
                    toRemove.Add(ho);
            }
            foreach (var ho in toRemove)
                HostileObjects.Remove(ho);
            toRemove.Clear();

            foreach (var hostile in _Context.Hostiles.Include(nameof(HostileObjectModel.Point)))
            {
                if (hostile.Point.Distance(selected.Point) < selected.RangedResourceTechnicalData.RangeRadius)
                    HostileObjects.Add(hostile);
            }
        }

        void TrackHostiles()
        {
            List<BaseObject> selected = new(HostileObjects.Where(x => x.Point.IsSelected));
            if (SelectedResource is RadarSystem rs && selected.Any())
            {
                SetHostilesTrackingInProgress(rs, true);
                rs.TrackHostilesExecute(ref selected, (_Context as PointsContext)!);
                SetHostilesTrackingInProgress(rs, false);
            }
        }

        #endregion

        #region Other Methods

        internal void SetStartingLocation(Point point)
        {
            Rectangle.X = point.X;
            Rectangle.Y = point.Y;
        }

        internal void SetIsMouseClickHold(bool isHolding)
        {
            Rectangle.IsMouseClickHold = isHolding;
        }

        internal void SetHeightAndWidth(Point point)
        {
            int height = (int)(point.Y - Rectangle.Y),
                width = (int)(point.X - Rectangle.X);
            Rectangle.Height = height < 0 ? 0 : height;
            Rectangle.Width = width < 0 ? 0 : width;
        }

        #endregion
    }


}


