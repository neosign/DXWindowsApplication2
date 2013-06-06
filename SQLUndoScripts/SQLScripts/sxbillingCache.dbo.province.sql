IF NOT EXISTS (SELECT * FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID(N'[dbo].[province]')) 
   ALTER TABLE [dbo].[province] 
   ENABLE  CHANGE_TRACKING
GO
