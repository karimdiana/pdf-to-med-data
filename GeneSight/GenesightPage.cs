// GenesightPage class provided by Jack

namespace PDFExtractor.GeneSight
{
    public class GenesightPage
    {
        public string Title { get; set; } = "";
        public string SmokingStatus { get; set; } = "";
        public List<GenesightDrug> UseAsDirected { get; set; } = new List<GenesightDrug>();
        public List<GenesightDrug> ModerateInteraction { get; set; } = new List<GenesightDrug>();
        public List<GenesightDrug> SignificantInteraction { get; set; } = new List<GenesightDrug>();
        public List<GenesightDrug> NoProvenMarkers { get; set; } = new List<GenesightDrug>();

        public GenesightPage(string Title, string SmokingStatus)
        {
            this.Title = Title;
            this.SmokingStatus = SmokingStatus;
        }

        public void AddUAD(GenesightDrug Drug)
        {
            UseAsDirected.Add(Drug);
        }

        public void AddMod(GenesightDrug Drug)
        {
            ModerateInteraction.Add(Drug);
        }

        public void AddSig(GenesightDrug Drug)
        {
            SignificantInteraction.Add(Drug);
        }

        public void AddNoProven(GenesightDrug Drug)
        {
            NoProvenMarkers.Add(Drug);
        }
    }
}
