using Ribbon;

var repo = new ModRepository("$2a$10$96h3LX0zd4NF9fTroK0Du.06R.mear0mOhpN.ax9B.8DUH.JM6A4K");
var res = await repo.SearchMods(0);
MyWindow window = new MyWindow();
window.AddData(res);
window.Show();
// ModView view = new();
// await view.Show(repo);

