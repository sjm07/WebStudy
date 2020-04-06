<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpLoadFile.aspx.cs" Inherits="WebForm.UpLoadFile" %>



<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>上传</title>
    <script type="text/javascript" src="js/jquery-1.11.0.min.js"></script>
    <style type="text/css">
        .btn {
            display: inline-block;
            width: 120px;
            height: 40px;
            cursor: pointer;
            text-align: center;
            line-height: 40px;
            background-color: #409EFF;
            color: #fff;
            border-radius: 4px;
            letter-spacing: .4em;
            vertical-align: middle;
            margin-left: 40px;
        }

        #fileUpLoad {
            background-color: orange;
            width: 400px;
            height: 200px;
            opacity: 0;
            cursor: pointer;
        }

        .uploadBox {
            display: inline-block;
            position: relative;
            vertical-align: middle;
            outline: 1px solid #606266;
        }

            .uploadBox div {
                position: absolute;
                top: 0;
                width: 100%;
                height: 100%;
            }

            .uploadBox .prompt {
                z-index: -2;
            }

            .uploadBox .fileName {
                z-index: -1;
                line-height: 26px;
                font-size: 14px;
                padding: 10px;
            }

            .uploadBox div p {
                text-align: center;
                color: #909399;
                line-height: 70px;
            }
    </style>
</head>
<body>
    <h2>ajax上传文件</h2>
    <div>
        <img id="preview" />
    </div>
    <div>
        <div class="uploadBox">
            <div class="prompt">
                <p><i>请点击此处上传文件</i></p>
                <p><i>或拖动文件到此处</i></p>
            </div>

            <div class="fileName"></div>

            <input type="file" name="fileName" id="fileUpLoad" accept="image/*">
        </div>
        <span class="btn">提交</span>
    </div>

    <script type="text/javascript">

        function imgPreview(fileDom) {
            //判断是否支持FileReader
            if (window.FileReader) {
                var reader = new FileReader();
            } else {
                alert("您的设备不支持图片预览功能，如需该功能请升级您的设备！");
            }
            //获取文件
            //var file = fileDom.files[0];
            var file = fileDom;
            var imageType = /^image\//;
            //是否是图片
            if (!imageType.test(file.type)) {
                alert("请选择图片！");
                return;
            }
            //读取完成
            reader.onload = function (e) {
                //获取图片dom
                var img = document.getElementById("preview");
                //图片路径设置为读取的图片
                img.src = e.target.result;
            };
            reader.readAsDataURL(file);
        }

        $(function () {

            // 检测是否选择文件，如果选择，返回文件对象;如果没选择，返回false
            function checkFile() {
                // 获取文件对象(该对象的类型是[object FileList]，其下有个length属性)
                var fileList = $('#fileUpLoad')[0].files;

                // 如果文件对象的length属性为0，就是没文件
                if (fileList.length === 0) {
                    console.log('没选择文件');
                    return false;
                };
                return fileList[0];
            };

            // 文件选择成功，显示文件名称
            $('#fileUpLoad').change(function () {
                var file = checkFile();
                if (!file) {
                    return false;
                };

                var name = $('#fileUpLoad')[0].files[0].name;
                $('.fileName').text(name);

                imgPreview(file);
            });

            $('.btn').click(function () {
                var file = checkFile();
                if (!file) {
                    alert('请先选择文件');
                    return false;
                };

                // 构建form数据
                var formFile = new FormData();
                //formFile.append("action", "UploadPath");
                //把文件放入form对象中  
                formFile.append("file", file);
                formFile.append("username", "sjm");

                $.ajax({
                    url: 'UpLoadFile.aspx?Method=Save',
                    dataType: "json",
                    data: formFile,
                    type: "POST",
                    cache: false,			//上传文件无需缓存
                    processData: false,		//用于对data参数进行序列化处理 这里必须false
                    contentType: false,

                    success: function (result) {
                        if (result.total >= 1) {

                        }
                        else {
                        }
                    }
                });

                // ajax提交
                //$.ajax({
                //	url: "",
                //   	data: formFile,
                //   	type: "POST",
                //   	dataType: "json",
                //   	cache: false,			//上传文件无需缓存
                //   	processData: false,		//用于对data参数进行序列化处理 这里必须false
                //   	contentType: false, 	//必须
                //   	success: function(result){
                //   		alert('上传成功');
                //   	},
                //   	error: function(result){
                //   		alert('上传失败');
                //   	}
                //});
            });
        })
</script>
</body>
</html>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="js/jquery-1.11.0.min.js"></script>
    <script>
        $(function () {
            $('#btnOK').click(function () {

                var formData = new FormData();
                formData.append('file', $('#ClubImagesUpload')[0].files[0]); // 固定格式

                alert("sjm");
                alert(formData);
            });
        });
    </script>
</head>
<body>
    <div>
        <input type="file" accept="image/*" name="ClubImagesUpload" id="ClubImagesUpload" class="hide" />
    </div>
    <div>

        <input id="btnOK" type="submit" value="提交" />
    </div>
</body>
</html>--%>
