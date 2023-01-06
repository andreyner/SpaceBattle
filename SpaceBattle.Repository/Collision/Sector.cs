using SpaceBattle.Repository.Container;
using SpaceBattle.Repository.MacroCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceBattle.Repository.Collision
{
    /// <summary>
    /// Окрестность
    /// </summary>
    public class Sector
    {
        public int Id;
        public Sector Neighbor;
        public List<Uobject> Spaceships;
        public double[] Coordinates;
        public ICommand CollisionsCheckMacroCommand;

        public Sector(int id, Sector neighbor, double[] coordinates)
        {
            Id = id;
            Neighbor = neighbor;
            Coordinates = coordinates;
            Spaceships = new List<Uobject>();
            CollisionsCheckMacroCommand = null;
        }

        /// <summary>
        /// Обновить списки космических кораблей по секторам
        /// </summary>
        /// <param name="spaceship"></param>
        public void UpdateSpaceshipList(Uobject spaceship)
        {
            string id = (string)spaceship["id"];
            Vector position = (Vector)spaceship["Positiion"];
            bool spaceshipInSector = SpaceShipInSector(position);

            if (spaceshipInSector)
            {
                // Если космический корабль попал в новую окрестность
                if (!Spaceships.Any(s => (string)s["id"] == id))
                {
                    Spaceships.Add(spaceship);
                    CollisionsCheckMacroCommand = PrepareCollisionsCheckMacroCommand(spaceship);
                }
            }
            else
            {
                if (Spaceships.Any(s => (string)s["id"] == id))
                {
                    Spaceships.Remove(spaceship);
                }

                // Проверим в соседней окресности.
                if (Neighbor != null)
                {
                    Neighbor.UpdateSpaceshipList(spaceship);
                }
            }

        }

        public bool SpaceShipInSector(Vector position)
        {
            return Coordinates.Contains(position[0]);
        }

        public ICommand PrepareCollisionsCheckMacroCommand(Uobject spaceship)
        {
            if (Spaceships.Count == 0)
            {
                return new MacroComamnd(new List<ICommand>());
            }

            List<ICommand> commands = new List<ICommand>();

            foreach (var sectorSpaceship in Spaceships)
            {
                ObjectCollisionCheckCommand objectCollisionCheckCommand = IoC.Resolve<ObjectCollisionCheckCommand>("ObjectCollisionCheckCommand", sectorSpaceship, spaceship);
                commands.Add(objectCollisionCheckCommand);
            }

            return new MacroComamnd(commands);
        }

        /// <summary>
        /// Стратегия, позволяет генерировать любые смещения координат по оси x для разных окресностей.
        /// </summary>
        /// <param name="shift"></param>
        /// <returns></returns>
        public static Sector[] InitializeSectors(int shift = 0)
        {
            var sectors = new Sector[4];
            Sector neighbor = null;

            for (int i = 3; i >= 0; i--)
            {
                List<double> coordinates = new List<double>();
                int coord = i * 10 + 1 + shift;
                int coordTen = coord + 9;
                for (int j = coord; j <= coordTen; j++)
                {
                    coordinates.Add(j);
                }

                var sector = new Sector(i + 1, neighbor, coordinates.ToArray());
                sectors[i] = sector;
                neighbor = sector;
            }

            return sectors;
        }
    }
}
