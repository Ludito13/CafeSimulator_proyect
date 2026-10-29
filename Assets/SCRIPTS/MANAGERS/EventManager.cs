using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class EventManager
{

    #region ACTION

    #region No parameters
    private static readonly Dictionary<(Type, int), Delegate> _event_dic_no_p = new Dictionary<(Type, int), Delegate>();

    public static void Suscribe(int id, Action callback)
    {
        if (_event_dic_no_p.TryGetValue((typeof(Action), id), out var del))
            _event_dic_no_p[(typeof(Action), id)] = (Action)del + callback;
        else
            _event_dic_no_p[(typeof(Action), id)] = callback;
    }

        public static void Unsuscribe(int id, Action callback)
    {
        if (_event_dic_no_p.TryGetValue((typeof(Action), id), out var del))
        {
            var current = (Action)del - callback;

            if (current == null) _event_dic_no_p.Remove((typeof(Action), id));
            else _event_dic_no_p[(typeof(Action), id)] = current;
        }
    }

    public static void Trigger(int id)
    {
        if (_event_dic_no_p.TryGetValue((typeof(Action), id), out var del)) ((Action)del)?.Invoke();
    }

    #endregion

    #region 1 parameter
    private static readonly Dictionary<(Type, int), Delegate> _event_Dic_onePar = new Dictionary<(Type, int), Delegate>();

    public static void Suscribe<T>(int id, Action<T> callback)
    {
        var key = (typeof(Action<T>), id);

        if (_event_Dic_onePar.TryGetValue(key, out var del))
            _event_Dic_onePar[key] = (Action<T>)del + callback;
        else
            _event_Dic_onePar[key] = callback;
    }

    public static void Unsuscribe<T>(int id, Action<T> callback)
    {
        var key = (typeof(Action<T>), id);

        if (_event_Dic_onePar.TryGetValue(key, out var del))
        {
            var current = (Action<T>)del - callback;

            if (current == null) _event_Dic_onePar.Remove(key);
            else _event_Dic_onePar[key] = current;
        }
    }

    public static void Trigger<T>(int id,T evnt)
    {
        if (_event_Dic_onePar.TryGetValue((typeof(Action<T>), id), out var del)) ((Action<T>)del)?.Invoke(evnt);
    }

    #endregion

    #region 2 Parameters
    private static readonly Dictionary<(Type, int), Delegate> _event_Dic_twoPar = new Dictionary<(Type, int), Delegate>();

    public static void Suscribe<T, R>(int id, Action<T, R> callback)
    {
        var key = (typeof(Action<T, R>), id);

        if (_event_Dic_twoPar.TryGetValue(key, out var del))
            _event_Dic_twoPar[key] = (Action<T, R>)del + callback;
        else
            _event_Dic_twoPar[key] = callback;
    }

    public static void Unsuscribe<T, R>(int id, Action<T, R> callback)
    {
        var key = (typeof(Action<T, R>), id);

        if (_event_Dic_twoPar.TryGetValue(key, out var del))
        {
            var current = (Action<T, R>)del - callback;

            if (current == null) _event_Dic_twoPar.Remove(key);
            else _event_Dic_twoPar[key] = current;
        }
    }

    public static void Trigger<T, R>(int id, T arg1, R arg2)
    {
        if (_event_Dic_twoPar.TryGetValue((typeof(Action<T, R>), id), out var del)) ((Action<T, R>)del)?.Invoke(arg1, arg2);
    }
    #endregion

    #region 3 parameters
    private static readonly Dictionary<(Type, int), Delegate> _event_Dic_threePar = new Dictionary<(Type, int), Delegate>();

    public static void Suscribe<T, R, S>(int id, Action<T, R, S> callback)
    {
        var key = (typeof(Action<T, R, S>), id);

        if (_event_Dic_threePar.TryGetValue(key, out var del))
            _event_Dic_threePar[key] = (Action<T, R, S>)del + callback;
        else
            _event_Dic_threePar[key] = callback;
    }

    public static void Unsuscribe<T, R, S>(int id, Action<T, R, S> callback)
    {
        var key = (typeof(Action<T, R, S>), id); 
        if (_event_Dic_threePar.TryGetValue(key, out var del))
        {
            var current = (Action<T, R, S>)del - callback;

            if (current == null) _event_Dic_threePar.Remove(key);
            else _event_Dic_threePar[key] = current;
        }
    }

    public static void Trigger<T, R, S>(int id, T arg1, R arg2, S arg3)
    {
        var key = (typeof(Action<T, R, S>), id); 

        if (_event_Dic_threePar.TryGetValue(key, out var del)) ((Action<T, R, S>)del)?.Invoke(arg1, arg2, arg3);
    }

    #endregion

    #endregion

    #region FUNC

    #region 1 parameter
    private static Dictionary<(Type, int), Delegate> _event_func_onepar = new Dictionary<(Type, int), Delegate>();

    public static void Suscribe<F>(int id, Func<F> callback)
    {
        var key = (typeof(Func<F>), id);

        if (_event_func_onepar.TryGetValue(key, out var del))
            _event_func_onepar[key] = (Func<F>)del + callback;
        else
            _event_func_onepar[key] = callback;
    }

    public static void Unsuscribe<F>(int id, Func<F> callback)
    {
        var key = (typeof(Func<F>), id);

        if (_event_func_onepar.TryGetValue(key, out var del))
            _event_func_onepar[key] = (Func<F>)del + callback;
        else
            _event_func_onepar[key] = callback;
    }

    public static F Trigger<F>(int id)
    {
        if (_event_func_onepar.TryGetValue((typeof(Func<F>), id), out var del) && del is Func<F> func)
        {
            if(func != null) return func.Invoke();                      
        }

        return default;
    }

    #endregion

    #region 2 Parameters
    private static Dictionary<(Type, int), Delegate> _event_func_twopar = new Dictionary<(Type, int), Delegate>();

    public static void Suscribe<F, G>(int id, Func<F, G> callback)
    {
        var key = (typeof(Func<F, G>), id);

        if (_event_func_twopar.TryGetValue(key, out var del))
            _event_func_twopar[key] = (Func<F, G>)del + callback;
        else
            _event_func_twopar[key] = callback;
    }

    public static void Unsuscribe<F, G>(int id, Func<F, G> callback)
    {
        var key = (typeof(Func<F, G>), id);

        if (_event_func_twopar.TryGetValue(key, out var del))
            _event_func_twopar[key] = (Func<F, G>)del + callback;
        else
            _event_func_twopar[key] = callback;
    }

    public static G Trigger<F, G>(int id, F arg1)
    {
        var key = (typeof(Func<F, G>), id);

        if (_event_func_twopar.TryGetValue(key, out var del) && del is Func<F, G> func)
        {
            if(func != null) return func.Invoke(arg1);                      
        }

        return default;
    }
    #endregion

    #region 3 parameters
    private static Dictionary<(Type, int), Delegate> _event_func_threepar = new Dictionary<(Type, int), Delegate>();

    public static void Suscribe<F, G, H>(int id, Func<F, G, H> callback)
    {
        var key = (typeof(Func<F, G, H>), id);

        if (_event_func_threepar.TryGetValue(key, out var del))
            _event_func_threepar[key] = (Func<F, G, H>)del + callback;
        else
            _event_func_threepar[key] = callback;
    }

    public static void Unsuscribe<F, G, H>(int id, Func<F, G, H> callback)
    {
        var key = (typeof(Func<F, G, H>), id);

        if (_event_func_threepar.TryGetValue(key, out var del))
            _event_func_threepar[key] = (Func<F, G, H>)del + callback;
        else
            _event_func_threepar[key] = callback;
    }

    public static H Trigger<F, G, H>(int id, F arg1, G arg2)
    {
        var key = (typeof(Func<F, G, H>), id);

        if (_event_func_threepar.TryGetValue(key, out var del) && del is Func<F, G, H> func)
        {
            if(func != null) return func.Invoke(arg1, arg2);                      
        }

        return default;
    }
    #endregion

    #endregion

}

