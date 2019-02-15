using UnityEngine;

using Zenject;

public class SampleSceneContainer : MonoInstaller<SampleSceneContainer> {
    public override void InstallBindings() {
        Container.Bind<APathfinder>().FromInstance(new AStarPathfinder());
        Container.Bind<Camera>().FromInstance(Camera.main);
        Container.Bind<VisualPath>().FromComponentInHierarchy().AsSingle().Lazy();
        Container.Bind<Field>().FromComponentInHierarchy().AsSingle();
    }
}
