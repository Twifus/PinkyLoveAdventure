/* Author : Raphaël Marczak - 2018, for ENSEIRB-MATMECA
 * 
 * This work is licensed under the CC0 License. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {
    public List<DialogPage> m_dialogWithPlayer;
    public int dialogID = -1;

    private void Start()
    {
        m_dialogWithPlayer = Dialogs.getDialogData(dialogID);
    }

    public List<DialogPage> GetDialog()
    {
        return m_dialogWithPlayer;
    }
}
