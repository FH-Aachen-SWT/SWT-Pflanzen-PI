using System.Globalization;
using sensor;

SensorService sensorService = new SensorService();

sensorService.Register(new MoistureSensor());
sensorService.Register(new BrightnessSensor());

var moistureSensor = sensorService.Get<Moisture>();
var brightnessSensor = sensorService.Get<Brightness>();

moistureSensor.OnDatenChanged += OnChange;
moistureSensor.OnDatenChanged += (_,_) => Console.WriteLine("Zweite version");
brightnessSensor.OnDatenChanged += (prev, newData) => Console.WriteLine($"Brightness changed! Prev: {prev} -> New: {newData}");

void OnChange(Moisture? prev, Moisture newData)
{
    Console.WriteLine($"Moisture changed! Prev: {prev} -> New: {newData}");
}

while (true)
{
    
}