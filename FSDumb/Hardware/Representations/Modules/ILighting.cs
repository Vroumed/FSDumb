using System.Drawing;

namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public interface ILighting
    {
        public byte LightCount { get; }
        public void TurnOff();
        public void Commit();
        public void Fill(Color color);

        public void ConnectingServer();
        public void ErrorServer();
        public void ServerOnline();
        public void ConnectingController();
        public void ErrorController();
        public void ControllerOnline();
        public void StandardLights();
    }
}
