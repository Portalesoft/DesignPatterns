/*     
 *  Simple fluent builder - http://www.stefanoricciardi.com/2010/04/14/a-fluent-builder-in-c/
 */

namespace DesignPatterns.Patterns.Builder {

    public class Team {

        public string Name { get; set; }
        public string NickName { get; set; }
        public string HomeTown { get; set; }
        public string Ground { get; set; }

        public Team(string name, string nickName, string homeTown, string ground) {
            Name = name;
            NickName = nickName;
            HomeTown = homeTown;
            Ground = ground;
        }

    }

    public class TeamBuilder {

        private string _name;
        private string _nickName;
        private string _homeTown;
        private string _ground;

        public static implicit operator Team(TeamBuilder tb) {
            return new Team(
                tb._name,
                tb._nickName,
                tb._homeTown,
                tb._ground);
        }

        public TeamBuilder CreateTeam(string name) {
            _name = name;
            return this;
        }

        public TeamBuilder WithNickName(string nickName) {
            _nickName = nickName;
            return this;
        }

        public TeamBuilder FromTown(string homeTown) {
            _homeTown = homeTown;
            return this;
        }

        public TeamBuilder PlayingAt(string ground) {
            _ground = ground;
            return this;
        }
    }
}
