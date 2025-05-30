using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFExtractor.Models.DataMining
{
    public class MedicationInteraction
    {
        public static readonly string[][] medicationsList = [
            [
                "amitriptyline (Elavil)", "bupropion (Wellbutrin)", "clomipramine (Anafranil)", "desipramine (Norpramin)",
                "desvenlafaxine (Pristiq)", "doxepin (Sinequan)", "fluoxetine (Prozac)", "imipramine (Tofranil)",
                "levomilnacipran (Fetzima)", "nortriptyline (Pamelor)", "selegiline (Emsam)", "trazodone (Desyrel)",
                "venlafaxine (Effexor)", "vilazodone (Viibryd)", "vortioxetine (Trintellix)", "duloxetine (Cymbalta)",
                "fluvoxamine (Luvox)", "mirtazapine (Remeron)", "citalopram (Celexa)", "escitalopram (Lexapro)",
                "paroxetine (Paxil)", "sertraline (Zoloft)"
                ],
            [
                "alprazolam (Xanax)", "buspirone (BuSpar)", "chlordiazepoxide (Librium)", "clonazepam (Klonopin)",
                "clorazepate (Tranxene)", "diazepam (Valium)", "eszopiclone (Lunesta)", "lemborexant (Dayvigo)",
                "lorazepam (Ativan)", "oxazepam (Serax)", "suvorexant (Belsomra)", "temazepam (Restoril)",
                "zolpidem (Ambien)", "propranolol (Inderal)"
                ],
            [
                "aripiprazole (Abilify)", "brexpiprazole (Rexulti)", "cariprazine (Vraylar)", "fluphenazine (Prolixin)",
                "iloperidone (Fanapt)", "lumateperone (Caplyta)", "lurasidone (Latuda)", "paliperidone (Invega)",
                "perphenazine (Trilafon)", "quetiapine (Seroquel)", "risperidone (Risperdal)", "ziprasidone (Geodon)",
                "asenapine (Saphris)", "chlorpromazine (Thorazine)", "haloperidol (Haldol)", "thioridazine (Mellaril)",
                "clozapine (Clozaril)", "olanzapine (Zyprexa)", "thiothixene (Navane)"
                ],
            [
                    "gabapentin (Neurontin)", "lithium (Eskalith)", "topiramate (Topamax)", "carbamazepine (Tegretol)",
                    "lamotrigine (Lamictal)","oxcarbazepine (Trileptal)", "valproic acid/divalproex (Depakote)"
                ],
            [
                    "amphetamine salts (Adderall)", "dexmethylphenidate (Focalin)", "dextroamphetamine (Dexedrine)",
                    "lisdexamfetamin (Vyvanse)", "methylphenidate (Ritalin, Concerta)"
                ],
            [
                    "atomoxetine (Strattera)", "clonidine (Kapvay)", "guanfacine (Intuniv)", "viloxazine (Qelbree)"
                ]
        ];

        public string Medication { get; set; } = string.Empty;
        public int UAD { get; set; } = 0;
        public int Mod { get; set; } = 0;
        public int Sig { get; set; } = 0;
    }
}
