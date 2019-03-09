/* Author : Raphaël Marczak - 2018, for ENSEIRB-MATMECA
 * 
 * This work is licensed under the CC0 License. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class is used to correctly display a full dialog
public static class Dialogs{

    /* PRIVATE */
    //To enable or disable dialogs
    static private bool enable = true;

    //For diaologs that are displayed only once
    static private bool gameBeginingHappened_0 = false;
    static private bool talkedToPNJ_6 = false;
    static private bool BOSS1_8 = false;
    static private bool BOSS1_9 = false;

    static private List<DialogPage> convertList(List<string> l, Color c)
    {
        List<DialogPage> returned = new List<DialogPage>(l.Count);
        DialogPage buffer = new DialogPage();
        int i;
        for (i = 0; i < l.Count; i++)
        {
            buffer.text = l[i];
            buffer.color = c;
            returned.Add(buffer);
        }
        return returned;
    

    /* PUBLIC */
    }
    static public List<DialogPage> m_dialogToDisplay;


    static void Awake () {

    }

    static public void enableDialogs()
    {
        enable = true;
    }

    static public void disableDialogs()
    {
        enable = false;
    }

    //To get the dialog corresponding to the dialogID into a displayable List<DialogPage>
    static public List<DialogPage> getDialogData(int dialogID)
    {
        List<string> dialogData = new List<string>();
        Color c = Color.black;
        List<DialogPage> returned = new List<DialogPage>();

        if (enable)
        {
            switch (dialogID)
            {
                case 0:
                    if (!gameBeginingHappened_0)
                    {
                        dialogData.Add("Aaaah.. ma tête\nJ'ai l'impression d'avoir dormi pendant des années...");
                        dialogData.Add("Mais..où suis-je ? Où sont passés ma maison et ma famille ?!");
                        dialogData.Add("Ahh...Je meurs de faim, heureusement que j'ai quelques cookies !");
                        gameBeginingHappened_0 = true;
                    }
                    break;
                case 1:
                    dialogData.Add("Monsieur bizarre : Aougaçhuibfbzibfezofpzjfenodzofbqfuqyfzgniqbggzgfz3.14zufbvaol !\n(Oh ! Salut toi ! Je vois que tu as des cookies ! Tu devrais les partager avec ces monstres, ce sont les derniers de leur espèce, il faut les nourrir)");
                    break;
                case 2:
                    dialogData.Add("Monsieur dubitatif : Oh non ! Ce monstre est bloqué, je ne peux pas l'atteindre !");
                    break;
                case 3:
                    dialogData.Add("Monsieur contemplatif : On raconte que jadis, la mer aussi était pleine de montres...");
                    break;
                case 4:
                    dialogData.Add("Monsieur bloquant : Halte ! Si tu veux entrer dans cette grotte il faudra me donner 6 coeurs de mopnstres !");
                    dialogData.Add("Monsieur bloquant : Comme la femme qui est passée un peu plus tôt, elle te ressemblait un peu d'ailleurs...");
                    break;
                case 5:
                    dialogData.Add("Monsieur peureux : La femme qui est partie là bas est très forte, trop forte..\nOn a dû l'enfermer avec cette barrière..");
                    break;
                case 6:
                    if (!talkedToPNJ_6)
                    {
                        dialogData.Add("Monsieur normal : LES MONSTRES SONT SI BON !!");
                        talkedToPNJ_6 = true;
                    }
                    else
                    {
                        dialogData.Add("Monsieur normal : Tiens, tu ressembles un peu à un monstre, mais pas complètement..");
                    }
                    break;
                case 7:
                    dialogData.Add("Madame ingénieur : Si tu veux que je rallume le courant dans la zonne il faudra m'offrir 3 coeurs de monstres.");
                    break;
                case 8:
                    if (!BOSS1_8)
                    {
                        dialogData.Add("Mais..ce monstre est gigantesque, il doit avoir extrêmement faim !Je vais le nourir, puis je vais retrouver ma maman, elle doit être quelque part par ici..");
                        dialogData.Add("Monstre géant : MANGEEEEERRRR !!");
                        BOSS1_8 = true;
                    }
                    break;
                case 9:
                    if (!BOSS1_9)
                    {
                        dialogData.Add("Monstre géant : Ah... ma fille... merci de m'avoir libérée de la mutation...");
                        dialogData.Add("Maman ! Mais.. tu saignes !!");
                        dialogData.Add("Maman : Quand tu as disparue... le monde a changé... la nature a repris ses droits et le réchauffement climatique a causé de nombreuses morts...");
                        dialogData.Add("Maman : Ceux qui ont survécu ont muté.. en monstres...");
                        dialogData.Add("Maman : Seuls les poulets n'ont as été affectés...");
                        dialogData.Add("Maman : Ah.. ma fille...");
                        dialogData.Add("Maman : Je suis si heureuse que tu ailles bien...");
                        dialogData.Add("Maman : je.. ton père...");
                        dialogData.Add("Maman ! Non ! Réponds moi !!");
                        dialogData.Add("Mais.. ce ne sont pas des cookies..nCe sont des scies !!");
                        dialogData.Add("Mais qu'est-ce que j'ai fait ?!");
                        dialogData.Add("Toutes ces personnes qui m'ont fait tuer des poulets...\nqui m'ont fait tuer ma maman !");
                        dialogData.Add("Je vais me venger, je vois clair maintenant !\nToutes ces illusions ne m'auront plus !");
                        BOSS1_9 = true;
                    }
            break;
                default:
                    returned = convertList(dialogData, c);
                    m_dialogToDisplay = returned;
                    return returned;
            }
        }
        returned = convertList(dialogData, c);
        m_dialogToDisplay = returned;
        return returned;
    }
}
