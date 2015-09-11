﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Partial Public Class DefaultContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=DefaultContext")
        MyBase.Configuration.LazyLoadingEnabled = False
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Overridable Property Advertisements() As DbSet(Of Advertisement)
    Public Overridable Property DownloadLinks() As DbSet(Of DownloadLink)
    Public Overridable Property EmailTemplates() As DbSet(Of EmailTemplate)
    Public Overridable Property EmailTemplateTypes() As DbSet(Of EmailTemplateType)
    Public Overridable Property FAQs() As DbSet(Of FAQ)
    Public Overridable Property Files() As DbSet(Of File)
    Public Overridable Property Menus() As DbSet(Of Menu)
    Public Overridable Property NewsSubscriptions() As DbSet(Of NewsSubscription)
    Public Overridable Property OrderDetails() As DbSet(Of OrderDetail)
    Public Overridable Property Orders() As DbSet(Of Order)
    Public Overridable Property ProductCategories() As DbSet(Of ProductCategory)
    Public Overridable Property ProductFiles() As DbSet(Of ProductFile)
    Public Overridable Property ProductPics() As DbSet(Of ProductPic)
    Public Overridable Property Products() As DbSet(Of Product)
    Public Overridable Property ProductTags() As DbSet(Of ProductTag)
    Public Overridable Property SentMails() As DbSet(Of SentMail)
    Public Overridable Property SlidePhotos() As DbSet(Of SlidePhoto)
    Public Overridable Property Tags() As DbSet(Of Tag)
    Public Overridable Property UserFavorites() As DbSet(Of UserFavorite)
    Public Overridable Property UserProductCategoryFavorites() As DbSet(Of UserProductCategoryFavorite)
    Public Overridable Property Users() As DbSet(Of User)

End Class
