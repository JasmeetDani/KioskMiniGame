using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<StartScreenController>().AsSingle().NonLazy();

        Container.Bind<GameController>().AsSingle().NonLazy();
        Container.Bind<ScreenFlasherController>().AsSingle().NonLazy();

        Container.Bind<HomeController>().AsSingle().NonLazy();
        Container.Bind<LeaderboardController>().AsSingle().NonLazy();
        Container.Bind<DataCollectionController>().AsSingle().NonLazy();
        Container.Bind<CountdownController>().AsSingle().NonLazy();
        Container.Bind<Stage1Controller>().AsSingle().NonLazy();
        Container.Bind<Stage2Controller>().AsSingle().NonLazy();
        Container.Bind<FinalStageController>().AsSingle().NonLazy();
        Container.Bind<CompletionController>().AsSingle().NonLazy();
        Container.Bind<FinalLeaderboardController>().AsSingle().NonLazy();
        Container.Bind<ThankYouController>().AsSingle().NonLazy();

        Container.Bind<ScreenFlasherViewModel>().FromComponentInHierarchy().AsSingle();

        Container.Bind<StartScreenViewModel>().FromComponentInHierarchy().AsSingle();

        Container.Bind<HomeViewModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LeaderboardViewModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DataCollectionViewModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CountdownViewModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Stage1ViewModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Stage2ViewModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<FinalStageViewModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CompletionViewModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<FinalLeaderboardViewModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ThankYouViewModel>().FromComponentInHierarchy().AsSingle();
    }
}