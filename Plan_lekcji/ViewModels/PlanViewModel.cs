using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plan_lekcji.DAL;

namespace Plan_lekcji.ViewModels
{
    public class PlanViewModel : Plan
    {
        public new IList<Przedmiot> Przedmiot { get; }
        public new IList<Godzina> Godzina { get;  }
        public IList<string> Dzien { get; }

        public PlanViewModel()
        {
            this.Przedmiot = new List<Przedmiot>();
            this.Godzina = new List<Godzina>();
        }

        public PlanViewModel(IEnumerable<Przedmiot> przedmiot, IEnumerable<Godzina> godziny, IEnumerable<string>  dni)
        {
            Przedmiot = przedmiot as IList<Przedmiot>;
            Godzina = godziny as IList<Godzina>;
            Dzien = dni as IList<string>;
        }
    }
}