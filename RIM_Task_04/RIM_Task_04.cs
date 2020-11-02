using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIM_Task_04
{
    // Base class to Player and Boss
    class Entity
    {
        // Constructor, that inits entitie`s health
        public Entity()
        {
            m_health = m_maxHealth;
        }

        // Returns current entitie`s health
        public int GetHealth()
        {
            return m_health;
        }

        // Sets current entitie`s health
        private void _setHealth(int health)
        {
            m_health = health;
        }

        // Checks if entity is still alive
        public bool isAlive()
        {
            return m_health > 0 ? true : false;
        }

        // Hits other entity for some damage
        protected void MakeDamage(Entity otherEntity, int damage)
        {
            if (otherEntity.GetHealth() - damage < 0)
                otherEntity._setHealth(0);
            else
                otherEntity._setHealth(otherEntity.GetHealth() - damage);
        }

        protected int m_health;
        protected const int m_maxHealth = 1000;
    }

    class Player : Entity
    {
        // Calls base class constructor and inits private fields
        public Player()
            : base()
        {
            m_isShadowShieldActivated = false;
            m_isShadowSpiritSummoned = false;
        }

        // Player makes turn (casts a spell)
        public void MakeTurn(Entity target)
        {
            Console.WriteLine("Enter the spell that you will cast: ");
            string spell = Console.ReadLine();

            this._getSpellFromInput(spell, target);
        }

        // Checks if player entered a spell
        private void _getSpellFromInput(string spell, Entity target)
        {
            switch(spell)
            {
            case "Fireball":
                this._castFireball(target);
                break;
            case "Baguvix":
                this._castBaguvix();
                break;
            case "Edo tensei":
                this._castEdoTensei();
                break;
            case "Soul attack":
                this._castSoulAttack(target);
                break;
            case "Shadow shield":
                this._castShadowShield();
                break;
            default:
                Console.WriteLine("Wrong spell name. You skip one turn.\n");
                break;
            }
        }

        // Handler for fireball spell
        private void _castFireball(Entity target)
        {
            Console.WriteLine("Fireball hits the boss for 50 damage.\n");
            this.MakeDamage(target, 50);
        }

        // Handler for baguvix spell
        private void _castBaguvix()
        {
            if (m_health <= 300 && m_isShadowSpiritSummoned)
            {
                Console.WriteLine("Baguvix cast success. \n");
                this.m_health += 250;
            }
            else
                Console.WriteLine("You cannot cast baguvix now.\n");
        }

        // Handler for edo tensei spell
        private void _castEdoTensei()
        {
            if (!m_isShadowSpiritSummoned)
            {
                Console.WriteLine("Alert! Shadow spirit was summoned. \n" +
                    "It takes one part of your soul, so you lose 100 health.\n");
                m_isShadowSpiritSummoned = true;
                m_health -= 100;
            }
            else
                Console.WriteLine("Shadow spirit is already with you. \n You don`t need to summon it again. \n");
        }

        // Handler for soul attack spell
        private void _castSoulAttack(Entity target)
        {
            if (m_isShadowSpiritSummoned)
            {
                this.MakeDamage(target, 350);
                Console.WriteLine("Your shadow spirit hits the boss for 350 health.\n");
                m_isShadowSpiritSummoned = false;
                Console.WriteLine("Shadow spririt goes away, he has done his job. \n");
            }
            else
                Console.WriteLine("You can`t use soul attack without shadow spirit. \n Try to summon it.\n");
        }

        // Handler for shadow shield spell
        private void _castShadowShield()
        {
            Console.WriteLine("Shadow shield activated. You restore 50 health \n");
            m_health += 50;
            m_isShadowShieldActivated = true;
        }

        // Deactivates shadow shield after boss attack
        public void ShadowShieldDeactivate()
        {
            Console.WriteLine("Shadow shield deactivated. \n");
            m_isShadowShieldActivated = false;
        }

        // Activates shadow shield until boss will attack 
        public bool isShadowShieldActive()
        {
            return m_isShadowShieldActivated;
        }

        private bool m_isShadowSpiritSummoned;
        private bool m_isShadowShieldActivated;
    }

    class Boss : Entity
    {
        // Boss hits player for 100 damage if there is no shadow shield activated
        public void MakeTurn(Player target)
        {
            if (!target.isShadowShieldActive())
            {
                this.MakeDamage(target, m_damage);
                Console.WriteLine($"Boss hit you for { m_damage } damage. \n");
            }
            else
                target.ShadowShieldDeactivate();
        }

        private const int m_damage = 100;
    }

    class RIM_Task_04
    {
        static void Main(string[] args)
        {
            // Creating a boss and a player
            Boss boss = new Boss();
            Player player = new Player();

            // Give some info about game to player
            Console.WriteLine("You are now in damp creepy dungeon. \nYou see something big standing in front of you.");
            Console.WriteLine("It is an ancient creature named Makhdee. He has 1000 health.");
            Console.WriteLine("To stay alive, you must kill it.");
            Console.WriteLine("To do this, you can cast 5 different spells.");
            Console.WriteLine("Fireball - you throw a fireball into Makhdee. Makhdee loses 50 health.");
            Console.WriteLine("Edo tensei - summons a shadow spirit, that takes a part of your soul (100 hp).");
            Console.WriteLine("Soul attack - your spirit hits the Makhdee for 350 damage and goes away.");
            Console.WriteLine("Shadow shield - you cover yourself with a shield and restore 50 health.");
            Console.WriteLine("Baguvix - if you are bleeding, your shadow spirit heals you for 250 health.");
            Console.WriteLine("LET THE FIGHT BEGIN. MAY GOD HAVE MERCY ON YOUR SOULS.");
            Console.WriteLine("-------------------------------------------------------------");

            // While both participants are alive, they make their turns
            while (player.isAlive() && boss.isAlive())
            {
                player.MakeTurn(boss);

                if(boss.isAlive())
                    boss.MakeTurn(player);

                Console.WriteLine("Player health = {0}", player.GetHealth());
                Console.WriteLine("Boss health = {0}", boss.GetHealth());
            }

            // Checking who won
            if(player.isAlive())
                Console.WriteLine("You were strong enough to defeat the boss. He is dead now. \n" +
                    "You found a chest full of gold, took it and went home.\n");
            else
                Console.WriteLine("Boss was stronger than you. He killed you. \n" +
                    "Your soul goes to hell.\n");

            Console.ReadKey();
        }
    }
}
