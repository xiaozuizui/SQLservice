namespace SQLlib.BASE
{

    public enum RETUEN
    {
        Add_Stu_Collection_Succeed, Add_Stu_Collection_Failed,
        Delete_Stu_Collection_Succeed,Delete_Stu_Collection_Failed,

        Add_Tips_User_Succeed,Add_Tips_User_Failed,
        Delete_Tips_User_Succeed,Delete_Tips_User_Failed,

        Add_Tips_Pro_Succeed,Add_Tips_Pro_Failed,
        Delete_Pro_Succeed,Delete_Pro_Failed,

        Query_NoRow,Query_HasRow,

        UpdateOwnerInfo_Succeed, UpdateOwnerInfo_Failed,
        GetProjectCards_Succeed, GetProjectCards_Failed,

        PublishProject_Succeed,PublishProject_Failed,

        GetProjectContent_Succeed, GetProjectContent_Failed,
        No_ERRO


    }
    public enum LABEL
    {
        CPP = 1 << 0,
        CSHARP = 1 << 1,
        PYTHON = 1 << 2,
        JAVA = 1 << 3,
        MATLAB = 1 << 4
    }

    public enum OPERATION
    {
        UpdateOwnerInfo = 1 << 0,
        GetGuestInfo = 1 << 1,


        PublishProject = 1 << 2,
        GetProjectCards = 1 << 3,

        GetProjectContent = 1 << 4,
        GetGuestInfoByTip = 1 << 5,


        SQL_STU = UpdateOwnerInfo | GetGuestInfo | GetGuestInfoByTip | GetProjectCards,
        SQL_PRO = PublishProject | GetProjectContent,
    }
}