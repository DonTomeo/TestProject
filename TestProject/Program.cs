using TestProject;

FixedList<Guid> a = new FixedList<Guid>(10);

Guid test = Guid.NewGuid();

a.Add(Guid.NewGuid());
a.Add(Guid.NewGuid());
a.Add(Guid.NewGuid());
a.Add(Guid.NewGuid());
a.Add(test);
a.Add(Guid.NewGuid());
a.Add(Guid.NewGuid());
a.Add(Guid.NewGuid());
a.Add(Guid.NewGuid());

foreach (var i in a) { Console.WriteLine(i); }

Console.ReadLine();

a.Remove(test);
foreach (var i in a) { Console.WriteLine(i); }

Console.ReadLine();

a.Add(test);
foreach (var x in a) { Console.WriteLine(x); }

Console.ReadLine();

a.Add(Guid.NewGuid());
foreach (var x in a) { Console.WriteLine(x); }

Console.ReadLine();