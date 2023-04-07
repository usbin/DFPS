using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract partial class BaseSkill : MonoBehaviour
{
    public class SkillManager
    {
        public delegate void OnAttackHandler(LivingEntity attacker, LivingEntity target, int damage);
        public OnAttackHandler OnAttack;
        Dictionary<string, Dictionary<string, BaseSkill>> _skillChains
            = new Dictionary<string, Dictionary<string, BaseSkill>>();
        public void AddTriggeredSkill(string skillName, BaseSkill triggeredSkill)
        {
            if (!_skillChains.ContainsKey(skillName))
            {
                _skillChains.Add(skillName, new Dictionary<string, BaseSkill>());

            }
            _skillChains[skillName].Add(triggeredSkill.SkillName, triggeredSkill);

        }
        public void RemoveTriggeredSkill(BaseSkill triggeredSkill)
        {
            //이 스킬이 발동시키는 체인 삭제
            _skillChains.Remove(triggeredSkill.SkillName);
            //이 스킬이 발동당하는 체인 삭제
            foreach (string key in _skillChains.Keys)
            {
                if (_skillChains[key].ContainsKey(triggeredSkill.SkillName))
                {
                    _skillChains[key].Remove(triggeredSkill.SkillName);
                }
            }

        }
        public void OnSetupSkill(BaseSkill skill, LivingEntity owner)
        {
            if(skill.OnSetup != null)
                skill.OnSetup(owner, this);
        }
        public void OnSetdownSkill(BaseSkill skill, LivingEntity owner)
        {
            if (skill.OnSetdown != null)
                skill.OnSetdown(owner, this);
        }
        public IEnumerator ExecuteSkill(BaseSkill skill, SkillArgs args, SkillManager skillManager)
        {
            if(skill.OnExecute != null)
            {
                //================
                // 해당 스킬 실행
                //================
                IEnumerator executeCorutine = skill.OnExecute(args, skillManager);
                object resultObj;
                SkillResult result;
                while (executeCorutine.MoveNext()) 
                {
                    yield return null;
                }
                resultObj = executeCorutine.Current;
                if(resultObj != null)
                {
                    result = (SkillResult)resultObj;

                    //result에 최종 값이 저장된 상태.


                    //===========================
                    // 해당 스킬의 후행 스킬 실행
                    //===========================
                    if (skill!=null && _skillChains.ContainsKey(skill.SkillName))
                    {
                        foreach (BaseSkill triggered in _skillChains[skill.SkillName].Values)
                        {
                            if (triggered.OnTriggered != null)
                            {
                                IEnumerator triggerCoroutine = triggered.OnTriggered(args, result, skillManager);
                                while (triggerCoroutine.MoveNext())
                                {
                                    yield return null;
                                }

                                resultObj = triggerCoroutine.Current;
                                if (resultObj != null)
                                {
                                    result = (SkillResult)resultObj;
                                }
                                else //결과가 저장되지 않았다면 다음 체인을 실행하지 않음.
                                {
                                    break;
                                }
                            }
                        }
                    }

                    
                }
            }
        }
    }
}