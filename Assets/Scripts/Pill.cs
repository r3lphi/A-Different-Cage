using UnityEngine;

public class Pill : Item
{
    public override void Use()
    {
        // switch (player.pillsTaken)
        // {
        //     case 0:
        //         messageManager.SendDialogue("“Weren’t there two other pills like this one? I feel…sleepy…");
        //         break;
        //     case 1:
        //         messageManager.SendDialogue("You took your second pill. The world feels like it's fading.");
        //         break;
        //     case 2:
        //         messageManager.SendDialogue("You took a third...");
        //         break;
        // }
        if (player.pillsTaken == 0)
        {
            messageManager.SendDialogue("Weren’t there two other pills like this one? I feel…sleepy…");
        }
        player.pillsTaken++;
        player.moveSpeed *= 0.5f;
        playerHoldingHand.DeleteHeldItem();

        if (player.pillsTaken == 3)
        {
            player.Kill();
        }
    }
}
