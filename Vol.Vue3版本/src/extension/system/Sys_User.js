import { defineAsyncComponent } from 'vue';
let extension = {
  components: {
    //动态扩充组件或组件路径
    //表单header、content、footer对应位置扩充的组件
    gridHeader: defineAsyncComponent(() =>
      import('./Sys_User/Sys_UserGridHeader.vue')
    ),
    gridBody: '',
    gridFooter: '',
    //弹出框(修改、编辑、查看)header、content、footer对应位置扩充的组件
    modelHeader: '',
    modelBody: '',
    modelFooter: ''
  },
  text: '只能看到当前角色下的所有帐号',
  buttons: [], //扩展的按钮
  methods: {
    //事件扩展
    onInit() {
      let btn = this.boxButtons.find((c) => {
        return c.value == 'save';
      });
      this.editFormOptions.forEach(x => {
        x.forEach(item => {
          if (item.field == 'HeadImageUrl') {
            item.type = 'file';//可以指定上传文件类型img/file/excel
            //设置上传图片字段100%宽度
            item.colSize = 12;
            //启用多图上传(默认单图)
            item.multiple = true;
            //禁止自动上传(默认自动上传)
            item.autoUpload=false;
            //最多可以上传3张照片
            item.maxFile = 3;
            //限制图片大小，默认3M
            item.maxSize = 3;
            //选择文件时
            item.onChange=(files)=>{
               console.log('选择文件事件')
               //此处不返回true，会中断代码执行
               return true;
            }
            //上传前
            item.uploadBefore=(files)=>{
           
              btn.disabled = !btn.disabled;
              return true;
            }
             //上传后
             item.uploadAfter=(files)=>{
              console.log('上传后')
              return true;
            }
          }
        })
      })

      this.boxOptions.height = 530;
      this.columns.push({
        title: '操作',
        hidden: false,
        align: 'center',
        fixed: 'right',
        width: 140,
        render: (h, { row, column, index }) => {
          return h(
            'div',
            {
              style: {
                'font-size': '13px',
                cursor: 'pointer',
                color: '#409eff'
              }
            },
            [
              h(
                'a',
                {
                  style: { 'margin-right': '15px' },
                  onClick: (e) => {
                    e.stopPropagation();
                    this.$refs.gridHeader.open(row);
                  }
                },
                '修改密码'
              ),
              h(
                'a',
                {
                  props: {},
                  style: {
                    'margin-left': '10px'
                  },

                  onClick: (e) => {
                    e.stopPropagation();
                    this.$refs.gridHeader.openRole(row);
                  }
                },
                '设置角色'
              )
            ]
          );
        }
      });
    },
    onInited() {},
    addAfter(result) {
      //用户新建后，显示随机生成的密码
      if (!result.status) {
        return true;
      }
      //显示新建用户的密码
      //2020.08.28优化新建成后提示方式
      this.$confirm(result.message, '新建用户成功', {
        confirmButtonText: '确定',
        type: 'success',
        center: true
      }).then(() => {});

      this.boxModel = false;
      this.refresh();
      return false;
    },
    modelOpenAfter() {
      //点击弹出框后，如果是编辑状态，禁止编辑用户名，如果新建状态，将用户名字段设置为可编辑
      let isEDIT = this.currentAction == this.const.EDIT;
      this.editFormOptions.forEach((item) => {
        item.forEach((x) => {
          if (x.field == 'UserName') {
            x.disabled = isEDIT;
          }
        });
        //不是新建，性别默认值设置为男
        if (!isEDIT) {
          this.editFormFields.Gender = '0';
        }
      });
    }
  }
};
export default extension;
