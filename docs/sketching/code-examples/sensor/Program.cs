using sensor;

SensorService sensorService = new SensorService();

sensorService.Register(new MoistureSensor());
sensorService.Register(new BrightnessSensor());

var moistureSensor = sensorService.Get<Moisture>();
var brightnessSensor = sensorService.Get<Brightness>();

moistureSensor.OnDatenChanged += (prev, newData) => Console.WriteLine($"Moisture changed! Prev: {prev} -> New: {newData}");
brightnessSensor.OnDatenChanged += (prev, newData) => Console.WriteLine($"Brightness changed! Prev: {prev} -> New: {newData}");

while (true)
{
    
}