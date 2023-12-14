<script language="javascrip" type="text/javascript">
    $(function () {
        $("#chonDuongDanAnh").click(function () {
            var ckfiner = new CKFinder();
            ckfiner.selectActionFunction = function (fileUrl) {
                $("#Anhdaidien").val(fileUrl);
            };
            ckfiner.popup();
        })
    });


    var ckeditor;
    function createEditor(languageCode, id) {
        var editor = CKEDITOR.replace(id, {language: languageCode });
    }
    $(function () {createEditor('vi', 'Mo')});
</script>