using UnityEngine;
using System;
using Agava.YandexGames;

public class LoadDataState : IGameState
{
    const string UserKey = "User";

    private readonly StateMashine _stateMashine;
    private readonly IUserStorage _userStorage;
    private readonly IDefaultUser _defaultUser;

    public LoadDataState(StateMashine stateMashine, IUserStorage userStorage, IDefaultUser defaultUser)
    {
        if(stateMashine == null)
            throw new ArgumentNullException(nameof(stateMashine));

        if (userStorage == null)
            throw new ArgumentNullException(nameof(userStorage));

        if (defaultUser == null)
            throw new ArgumentNullException(nameof(defaultUser));

        _stateMashine = stateMashine;
        _userStorage = userStorage;
        _defaultUser = defaultUser;
    }

    public void Execute()
    {
        Load();
    }

    private void Load()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
                LoadCloud();
            else
                LoadPlayerPrefs();
#else
        LoadPlayerPrefs();
#endif
    }

    private void LoadPlayerPrefs()
    {
        string json = PlayerPrefs.GetString(UserKey);
        UserModel user = GetDeserialize(json);
        _userStorage.SetUser(user);

        _stateMashine.EnterIn<LoadMainMenuSceneState>();
    }

    private void LoadCloud()
    {
        PlayerAccount.GetCloudSaveData(onSuccessCallback: (json) =>
        {
            UserModel user = GetDeserialize(json);
            _userStorage.SetUser(user);

            _stateMashine.EnterIn<LoadMainMenuSceneState>();
        });
    }

    private UserModel GetDeserialize(string json)
    {
        return _defaultUser.GetUser();

        if (string.IsNullOrEmpty(json))
            return _defaultUser.GetUser();

        return JsonUtility.FromJson<UserModel>(json);
    }
}
