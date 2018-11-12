namespace Todos.Interfaces

open Todos.Models

type TodoSave = Todo -> Todo

type TodoCriteria = 
    |All

type TodoFind = TodoCriteria -> Todo[]

type TodoDelete = string -> bool