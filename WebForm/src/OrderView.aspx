<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderView.aspx.cs" Inherits="WebForm.src.OrderView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>工单展示页面</title>
    <link href="../Style/bootstrap.min.css" rel="stylesheet" />
    <link href="../Style/ui.jqgrid.css" rel="stylesheet" />
    <link href="../Style/ui.jqgrid-bootstrap.css" rel="stylesheet" />
   <%-- <link href="../Style/ui.jqgrid-bootstrap-ui.css" rel="stylesheet" />
    <link href="../Style/ui.jqgrid-bootstrap4.css" rel="stylesheet" />--%>
    <script src="../js/jquery-1.11.0.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery.jqGrid.min.js"></script>
    <script type="text/javascript">
     
        $(function () {
            query();
        });

        LinkPage = function(rowId)
        {
            var rowData = $("#gridTable").jqGrid('getRowData', rowId);
            alert(rowData.name);
        }

        query = function () {
            $("#gridTable").jqGrid({  
                dataType:'local',         
                colNames:["国家/地区/组织代码","国家/地区/组织名称","操作","国家/地区/组织代码","国家/地区/组织名称","操作"],  
                colModel:[  
                {name: "code", index: "code", align: 'center',
                formatter: function (value, row, index) {
                    var rowId = row.rowId;
                        var view = '<a href="javascript:LinkPage('+ rowId +')">' + value + '</a>'
                        return view;
                    }
                },
                {name:"name",index:"name",align:'center'},  
                {name:"operation",index:"operation",align:'center'},  
                {name:"code1",index:"code1",align:'center'},  
                {name:"name1",index:"name1",align:'center'},  
                {name:"operation1",index:"operation1",align:'center'} 
                ],
                //multiselect: true,
                pagination: true,
                singleSelect: true,
                fitColumns: true,
                showFooter: true,
                viewrecords:true,  
                rowNum:5,  
                autoHeight: true,
                height: $(this).height(),
                rowList:[15,20,25,30],  
                jsonReader:{  
                    root: "rows",   
                    page: "page",   
                    total: "total",   
                    records: "records",   
                    repeatitems: false   
                }       
            });  
      
            var rows = [  
            {"code":"CN","name":"中国","operation":"<span style='color:blue;cursor:pointer;'>应用</span>","code1":"US","name1":"美国","operation1":"<span style='color:blue;cursor:pointer;'>应用</span>"},  
            {"code":"JP","name":"日本","operation":"<span style='color:blue;cursor:pointer;'>应用</span>","code1":"KR","name1":"韩国","operation1":"<span style='color:blue;cursor:pointer;'>应用</span>"},  
            {"code":"RU","name":"俄罗斯联邦","operation":"<span style='color:blue;cursor:pointer;'>应用</span>","code1":"EP","name1":"欧洲专利局","operation1":"<span style='color:blue;cursor:pointer;'>应用</span>"},  
            {"code":"WO","name":"世界知识产权组织","operation":"<span style='color:blue;cursor:pointer;'>应用</span>","code1":"AD","name1":"安道尔","operation1":"<span style='color:blue;cursor:pointer;'>应用</span>"},  
            {"code":"AE","name":"阿拉伯联合酋长国","operation":"<span style='color:blue;cursor:pointer;'>应用</span>","code1":"AF","name1":"阿富汗","operation1":"<span style='color:blue;cursor:pointer;'>应用</span>"},  
            {"code":"AG","name":"安提瓜和巴布达","operation":"<span style='color:blue;cursor:pointer;'>应用</span>","code1":"AI","name1":"安圭拉","operation1":"<span style='color:blue;cursor:pointer;'>应用</span>"},  
            {"code":"AL","name":"阿尔巴尼亚","operation":"<span style='color:blue;cursor:pointer;'>应用</span>","code1":"AM","name1":"亚美尼亚","operation1":"<span style='color:blue;cursor:pointer;'>应用</span>"},  
            {"code":"AN","name":"荷属安的列斯群岛","operation":"<span style='color:blue;cursor:pointer;'>应用</span>","code1":"AO","name1":"安哥拉","operation1":"<span style='color:blue;cursor:pointer;'>应用</span>"},  
            {"code":"AP","name":"非洲地区工业产权组织（ARIPO）","operation":"<span style='color:blue;cursor:pointer;'>应用</span>","code1":"AR","name1":"阿根廷","operation1":"<span style='color:blue;cursor:pointer;'>应用</span>"},  
            {"code":"AS","name":"美属萨摩亚","operation":"<span style='color:blue;cursor:pointer;'>应用</span>","code1":"AT","name1":"奥地利","operation1":"<span style='color:blue;cursor:pointer;'>应用</span>"} 
            ];  
            for(var i=0;i<rows.length;i++){  
                $("#gridTable").jqGrid('addRowData',i+1,rows[i]);  
            }  
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="gridTable">
        </table>
    </div>
    </form>
</body>
</html>
