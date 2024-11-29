using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector playableDirector; // Arraste o PlayableDirector aqui pelo Inspector.
    private List<TimelineClip> clips; // Lista de fragmentos.

    void Start()
    {
        // Obtenha os clipes do Timeline que já está no PlayableDirector.
        TimelineAsset timeline = (TimelineAsset)playableDirector.playableAsset;

        clips = new List<TimelineClip>();
        foreach (var track in timeline.GetOutputTracks())
        {
            foreach (var clip in track.GetClips())
            {
                clips.Add(clip);
            }
        }

        // Ordene os clipes por tempo de início.
        clips.Sort((a, b) => a.start.CompareTo(b.start));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clique do mouse ou toque.
        {
            AdvanceToNextFragment();
        }
    }

    void AdvanceToNextFragment()
    {
        // Obtenha o tempo atual do PlayableDirector.
        double currentTime = playableDirector.time;

        // Encontre o próximo fragmento com base no tempo atual.
        for (int i = 0; i < clips.Count; i++)
        {
            if (currentTime < clips[i].start)
            {
                playableDirector.time = clips[i].start; // Avança para o início do próximo fragmento.
                playableDirector.Evaluate(); // Atualiza imediatamente o estado do Timeline.
                return;
            }
        }

        // Se não houver mais fragmentos, finalize a cutscene.
        EndCutscene();
    }

    void EndCutscene()
    {
        Debug.Log("Cutscene finalizada!");
        playableDirector.Stop(); // Para a cutscene.
    }
}