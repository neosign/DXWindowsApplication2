IF NOT EXISTS (SELECT * FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID(N'[dbo].[district]')) 
   ALTER TABLE [dbo].[district] 
   ENABLE  CHANGE_TRACKING
GO
