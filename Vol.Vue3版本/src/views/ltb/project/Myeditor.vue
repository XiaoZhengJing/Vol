<template>
    <div style="border: 1px solid #ccc;">
        <Toolbar
            style="border-bottom: 1px solid #ccc"
            :editor="editor"
            :defaultConfig="toolbarConfig"
            :mode="mode"
        />
        <Editor
            style="height: 500px; overflow-y: hidden;"
            v-model="html"
            :defaultConfig="editorConfig"
            :mode="mode"
            @onCreated="onCreated"
            @onChange="onChange"
        />
    </div>

    <div>
        <!-- <el-button @click="dialogMaterial = false">Cancel</el-button> -->
        <el-button type="primary" @click="MaterialToDialog()">
            Confirm
        </el-button>
    </div>
</template>

<script>
import Vue from 'vue'
import { Editor, Toolbar } from '@wangeditor/editor-for-vue'
import { DomEditor } from '@wangeditor/editor'

export default({
    components: { Editor, Toolbar },
    data() {
        return {
            editor: null,
            html: '',
            toolbarConfig: { },
            editorConfig: { placeholder: 'Only Counter Material and Description Counter Material in Excel can be copied...' },
            mode: 'simple', // or 'simple'  default
        }
    },
    methods: {
        onCreated(editor) {
            this.editor = Object.seal(editor) // 一定要用 Object.seal() ，否则会报错
            //隐藏工具栏,在excludeKeys内加入key即可
            this.toolbarConfig = {
              excludeKeys: [
                  "blockquote","header1","header2","header3",
                  "bold","underline","italic",
                  "through","color","bgColor",
                  "clearStyle","bulletedList","numberedList","todo",
                  "justifyLeft","justifyRight","justifyCenter",
                  "insertLink","group-image","insertVideo","insertTable",
                  "codeBlock","undo","redo","fullScreen","|",
              ]
            }
        },
        onChange(editor) {
           const toolbar = DomEditor.getToolbar(editor)
           console.log("工具栏配置", toolbar.getConfig().toolbarKeys); // 显示工具栏配置
        },
        MaterialToDialog(){
            console.log("发送出去的值:",this.html);
            this.$emit('undataMaterial',this.html); //把值传给父组件
            //传过去之后清空
            setTimeout(() => {
               this.html = '';
            }, 2000)
        },
    },
    mounted() {
        // 模拟 ajax 请求，异步渲染编辑器
        // setTimeout(() => {
        //     this.html = '<p>模拟 Ajax 异步设置内容 HTML</p>'
        // }, 1500)
        this.html=""; //清空
    },
    beforeDestroy() {
        const editor = this.editor
        if (editor == null) return
        editor.destroy() // 组件销毁时，及时销毁编辑器
    }
})
</script>

<style src="@wangeditor/editor/dist/css/style.css"></style>