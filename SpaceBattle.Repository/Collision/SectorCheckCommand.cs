using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Collision
{
    public class SectorCheckCommand : ICommand
    {
        private Uobject Spaceship;
        private Sector FirstSector;

        public SectorCheckCommand(Uobject spaceship, Sector firstSector)
        {
            Spaceship = spaceship;
            FirstSector = firstSector;
        }

        public void Execute()
        {
            FirstSector.UpdateSpaceshipList(Spaceship);
        }
    }
}
