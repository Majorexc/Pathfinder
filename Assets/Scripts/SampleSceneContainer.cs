using Zenject;

public class SampleSceneContainer : MonoInstaller<SampleSceneContainer> {
    public override void InstallBindings() {
        Container.Bind<APathfinder>().FromInstance(new AStarPathfinder());
        Container.Bind<VisualPath>().FromComponentInHierarchy().AsSingle().Lazy();
    }
}
