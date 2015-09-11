'Imports Web.Repositories.EF
'Imports System.Data.Entity

'Public Class Logger

'    Private rep As GenericRepository(Of ActionLog, Integer)

'    Public Sub New(dbContext As DefaultContext)
'        rep = New GenericRepository(Of ActionLog, Integer)(dbContext)
'    End Sub

'    Public Sub Commit()
'        rep.Commit()
'    End Sub

'    Public Sub Log(project As Project, projectAction As ProjectActions)

'        Dim description As String = String.Empty

'        Select Case projectAction
'            Case ProjectActions.Add
'                description = String.Format("New project ({0} - {1}) has been added.", project.ProjectId, project.Title)

'            Case ProjectActions.Remove
'                description = String.Format("Project ({0} - {1}) has been removed.", project.ProjectId, project.Title)

'            Case ProjectActions.Update
'                description = String.Format("Project ({0} - {1}) has been updated.", project.ProjectId, project.Title)

'        End Select

'        Dim actionLog As New ActionLog
'        With actionLog

'            .ActionDate = Now
'            .ActionTypeId = projectAction
'            .Description = description
'            .EntityId = project.ProjectId
'            .EntityTypeId = EntityTypes.Project
'            .UserId = Application.GetCurrentUserId

'        End With

'        rep.Add(actionLog)
'        Commit()

'    End Sub

'    Public Sub Log(userStory As UserStory, userStoryAction As UserStoryActions)

'        Dim description As String = String.Empty

'        Select Case userStoryAction
'            Case UserStoryActions.Add
'                description = String.Format("New user story ({0} - {1}) has been added.", userStory.UserStoryId, userStory.Title)

'            Case UserStoryActions.Remove
'                description = String.Format("user story ({0} - {1}) has been removed.", userStory.UserStoryId, userStory.Title)

'            Case UserStoryActions.Update
'                description = String.Format("user story ({0} - {1}) has been updated.", userStory.UserStoryId, userStory.Title)

'        End Select

'        Dim actionLog As New ActionLog
'        With actionLog

'            .ActionDate = Now
'            .ActionTypeId = userStoryAction
'            .Description = description
'            .EntityId = userStory.UserStoryId
'            .EntityTypeId = EntityTypes.UserStory
'            .UserId = Application.GetCurrentUserId

'        End With

'        rep.Add(actionLog)
'        Commit()

'    End Sub

'    Public Sub Log(sprint As Sprint, sprintAction As SprintActions)

'        Dim description As String = String.Empty

'        Select Case sprintAction
'            Case SprintActions.Add
'                description = String.Format("New sprint ({0} - {1}) has been added.", sprint.SprintId, sprint.Title)

'            Case SprintActions.Remove
'                description = String.Format("Sprint ({0} - {1}) has been removed.", sprint.SprintId, sprint.Title)

'            Case SprintActions.Update
'                description = String.Format("Sprint ({0} - {1}) has been updated.", sprint.SprintId, sprint.Title)

'        End Select

'        Dim actionLog As New ActionLog
'        With actionLog

'            .ActionDate = Now
'            .ActionTypeId = sprintAction
'            .Description = description
'            .EntityId = sprint.SprintId
'            .EntityTypeId = EntityTypes.UserStory
'            .UserId = Application.GetCurrentUserId

'        End With

'        rep.Add(actionLog)
'        Commit()

'    End Sub

'    Public Sub Log(task As Task, taskAction As TaskActions)

'        Dim description As String = String.Empty

'        Select Case taskAction
'            Case TaskActions.Add
'                description = String.Format("New task ({0} - {1}) has been added.", task.TaskId, task.Title)

'            Case TaskActions.Remove
'                description = String.Format("Task ({0} - {1}) has been removed.", task.TaskId, task.Title)

'            Case TaskActions.Update
'                description = String.Format("Task ({0} - {1}) has been updated.", task.TaskId, task.Title)

'            Case TaskActions.StatusChanged
'                description = String.Format("Task ({0} - {1})'s status has been changed to {2}.", task.TaskId, task.Title, task.Status)

'            Case TaskActions.Archived
'                description = String.Format("Task ({0} - {1}) is archived.", task.TaskId, task.Title)

'        End Select

'        Dim actionLog As New ActionLog
'        With actionLog

'            .ActionDate = Now
'            .ActionTypeId = taskAction
'            .Description = description
'            .EntityId = task.TaskId
'            .EntityTypeId = EntityTypes.Task
'            .UserId = Application.GetCurrentUserId

'        End With

'        rep.Add(actionLog)
'        Commit()

'    End Sub

'    Public Sub Log(activity As Activity, activityAction As ActivityActions)

'        Dim description As String = String.Empty

'        Select Case activityAction
'            Case ActivityActions.Add
'                description = String.Format("New activity ({0} - {1}) has been added.", activity.ActivityId, activity.Description)

'            Case ActivityActions.Remove
'                description = String.Format("Activity ({0} - {1}) has been removed.", activity.ActivityId, activity.Description)

'            Case ActivityActions.Update
'                description = String.Format("Activity ({0} - {1}) has been updated.", activity.ActivityId, activity.Description)

'        End Select

'        Dim actionLog As New ActionLog
'        With actionLog

'            .ActionDate = Now
'            .ActionTypeId = activityAction
'            .Description = description
'            .EntityId = activity.ActivityId
'            .EntityTypeId = EntityTypes.Activity
'            .UserId = Application.GetCurrentUserId

'        End With

'        rep.Add(actionLog)
'        Commit()

'    End Sub

'    Public Sub Log(user As User, userAction As UserActions)

'        Dim description As String = String.Empty

'        Select Case userAction
'            Case UserActions.Add
'                description = String.Format("New user ({0} - {1}) has been added.", user.Id, user.UserName)

'            Case UserActions.Remove
'                description = String.Format("User ({0} - {1}) has been removed.", user.Id, user.UserName)

'            Case UserActions.Update
'                description = String.Format("User ({0} - {1}) has been updated.", user.Id, user.UserName)

'        End Select

'        Dim actionLog As New ActionLog
'        With actionLog

'            .ActionDate = Now
'            .ActionTypeId = userAction
'            .Description = description
'            .EntityId = user.Id
'            .EntityTypeId = EntityTypes.User
'            .UserId = Application.GetCurrentUserId

'        End With

'        rep.Add(actionLog)
'        Commit()

'    End Sub

'    Public Function GetByEntity(entityIds As IEnumerable(Of Integer), entityType As EntityTypes) As IList(Of ActionLog)

'        Return (From p In rep.GetAll.Include(Function(x) x.User) Where p.EntityTypeId = entityType AndAlso entityIds.Contains(p.EntityId)).ToList

'    End Function

'End Class
