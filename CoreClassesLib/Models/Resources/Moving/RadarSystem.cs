using CoreClassesLib.Contexts;
using CoreClassesLib.Models.Events;
using CoreClassesLib.Models.Points;
using MapControl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreClassesLib.Models.Resources.Moving
{
    public class RadarSystem : RangedResourceModel
    {
        public RadarSystem() :base()
        {
            OnBeforeTrackHostilesExecuted += RadarSystem_OnBeforeTrackHostilesExecuted;
        }

        public RadarSystem(string name, double latitude, double longitude, bool isHostileNeeded, double rangeRadius, double maxCeilingRange) : base(name, latitude, longitude, isHostileNeeded, rangeRadius, maxCeilingRange)
        {
            //RangeRadius = 40000;
            //MaxCeilingRange = 8000;
            //_MaxTargetsTracked = 4;
            //MaxAmmoCapacity = 0;
            RadarAzimuth = 0;
            //MultiTargetHandling = true;

            OnBeforeTrackHostilesExecuted += RadarSystem_OnBeforeTrackHostilesExecuted;
        }

        private int _RadarAzimuth;

        public int RadarAzimuth
        {
            get => _RadarAzimuth;
            set
            {
                if (_RadarAzimuth != value)
                {
                    _RadarAzimuth = value;
                    RaisePropertyChanged(nameof(RadarAzimuth));
                }
            }
        }

        private bool _HostilesTrackingInProgress;
        [NotMapped]
        public bool HostilesTrackingInProgress
        {
            get => _HostilesTrackingInProgress;
            set
            {
                if (_HostilesTrackingInProgress != value)
                {
                    _HostilesTrackingInProgress = value;
                    RaisePropertyChanged(nameof(HostilesTrackingInProgress));
                }
            }
        }


        //public bool LocateHostile(BasePointModel point)
        //{
        //    //Obsluga sparametryzowanej ilosci obrotow radaru, zmiana azymutu radaru
        //    double b = RangeRadius / Math.Cos((Math.PI / 180) * 30);
        //    int azimuth1 = RadarAzimuth + 30 >= 360 ? RadarAzimuth + 30 - 360 : RadarAzimuth + 30,
        //        azimuth2 = RadarAzimuth - 30 < 0 ? 360 - (RadarAzimuth + 30) : RadarAzimuth - 30;
        //    Location A = Location.GetLocation(azimuth1, b);
        //    Location B = Location.GetLocation(azimuth2, b);
        //    if (PointInTriangle(point.Location, Location, A, B))
        //        return true;
        //    return false;


        //    //    a = RangeRadius * Math.Tan(60);
        //    //List<HostileObjectModel> detected = new();
        //    //int index = 0;
        //    //do
        //    //{
        //    //    Thread.Sleep(1000);
        //    //    index++;
        //    //    //fired.Location = fired.MovePointTowards(hostile, fired.Speed, 500, ref distanceTraveled);
        //    //    //distance = fired.Distance(hostile);
        //    //} while (index < 60);
        //}

        public virtual void TrackHostiles(ref List<BaseObject> targets, PointsContext context)
        {
            if (targets.Any())
            {
                int index = 0;
                List<BaseObject> toRemove = new();
                int targetsCount = RangedResourceTechnicalData.MaxTargetsHandling > targets.Count ? targets.Count : RangedResourceTechnicalData.MaxTargetsHandling;
                do
                {
                    for (int i = 0; i < targetsCount; i++)
                    //foreach (var v in targets)
                    {
                        int id = targets[i].BaseObjectId;
                        var found = context.Hostiles.Include(nameof(Point)).FirstOrDefault(x => x.BaseObjectId == id);
                        context.Entry(found.Point).Reload();
                        Location tempLocation = new Location(found.Point.Location.Latitude, found.Point.Location.Longitude);
                        (targets[i] as HostileObjectModel).LatestTrackTime = DateTime.Now;
                        if (Point.Distance(tempLocation) < RangedResourceTechnicalData.RangeRadius)
                        {
                            targets[i].Point.Location = tempLocation;
                            targets[i].Point.Visible = true;
                        }
                        else
                            toRemove.Add(targets[i]);
                        foreach (var del in toRemove)
                            targets.Remove(del);
                        index++;
                        
                    }
                    Thread.Sleep(1000);
                } while (index < 30);
            }
        }

        public void TrackHostilesExecute(ref List<BaseObject> targets, PointsContext context)
        {
            OnBeforeTrackHostilesExecuted?.Invoke(targets, context);
            TrackHostiles(ref targets, context);
        }

        public event Action<List<BaseObject>, IPointsContext> OnBeforeTrackHostilesExecuted;

        protected virtual void RadarSystem_OnBeforeTrackHostilesExecuted(List<BaseObject> targets, IPointsContext context)
        {
            Command trackCommand = new Command()
            {
                ExecutionDate = DateTime.Now,
                Executor = this,
                CommandText = "Komenda: śledzenie celu/ów"
            };

            Commands.Add(trackCommand);
        }

        public override string ShowInformation()
        {
            return base.ShowInformation() + "\r\nMax śledzonych celów: " + RangedResourceTechnicalData.MaxTargetsHandling;
        }
    }
}
