<template>
  <div>
    <vol-box
      v-model="model"
      :padding="30"
      title="修改密码"
      :width="500"
      :height="250"
    >
      <el-alert type="success">
        <h3>
          <span>帐号：{{ row.UserName }}</span>
          <span>用户：{{ row.UserTrueName }}</span>
        </h3>
      </el-alert>
      <div>
        <el-input
          placeholder="请输入密码"
          v-model="password"
          size="large"
          style="width: 100%; margin-top: 15px"
        />
      </div>
      <template #footer>
        <el-button
          type="priarmy"
          size="mini"
          icon="md-checkmark-circle"
          @click="savePwd()"
          >修改密码</el-button
        >
        <el-button
          type="priarmy"
          size="mini"
          icon="md-close"
          @click="model = false"
          >关闭</el-button
        >
      </template>
    </vol-box>

    <vol-box
      v-model="role.model"
      :padding="20"
      title="设置角色"
      :width="450"
      :height="550"
    >
      <h3 class="user_name">帐号：{{ role.row.UserName }}</h3>
      <!--   <h3 class="item">选择角色</h3> -->
      <div class="item role-list">
        <el-tree
          :data="roles"
          :props="defaultProps"
          :expand-on-click-node="false"
          default-expand-all
        >
          <template #default="{ node, data }">
            <div class="action-group">
              <div class="action-text">{{ data.label }}</div>
              <div class="action-item">
                <el-checkbox v-model="roleIds[data.id]"></el-checkbox>
              </div>
            </div>
          </template>
        </el-tree>
      </div>
      <template #footer>
        <div style="text-align: center">
          <el-button
            type="priarmy"
            size="mini"
            icon="md-close"
            @click="role.model = false"
            >关闭</el-button
          >
          <el-button size="mini" type="primary" @click="saveRole"
            >保存</el-button
          >
        </div>
      </template>
    </vol-box>
  </div>
</template>
<script>
import { defineComponent, defineAsyncComponent } from 'vue';
export default defineComponent({
  components: {
    VolBox: defineAsyncComponent(() => import('@/components/basic/VolBox.vue'))
  },
  data() {
    return {
      defaultProps: {
        children: 'children',
        label: 'label'
      },
      row: {},
      password: '',
      model: false,
      source:[],
      role: {
        row: {},
        model: false
      },
      roles: [],
      roleIds: {}
    };
  },
  methods: {
    openRole(row) {
      if (!this.source.length) {
        this.getRoles();
      } else {
        this.source.forEach((c) => {
          c.checked = false;
        });
      }
      this.role.row = row;
      this.getUserRoles();
      this.role.model = true;
    },
    getRoles() {
      //获取系统所有的角色
      this.http.post('/api/User/getRoles').then((result) => {
        // var checkObj = {};
        result.forEach((element) => {
          this.roleIds[element.id] = false;
          element.checked = false;
        });
        this.source = result;
        // this.role.role.checked = checkObj;
        this.roles.push(
          ...this.base.convertTree(result, (node, data, isRoot) => {
            node.label = node.roleName;
          })
        );
      });
    },
    getUserRoles() {
      //获取当前选择行用户的角色id
      this.http
        .post(
          '/api/User/getUserRoles?userId=' + this.role.row.User_Id,
          {},
          true
        )
        .then((result) => {
          for (const key in this.roleIds) {
            this.roleIds[key] = result.indexOf(key * 1) != -1;
          }
        });
    },
    saveRole() {
      var checkedIds = [];
      for (const key in this.roleIds) {
        if (this.roleIds[key]) {
          checkedIds.push(key);
        }
      }
      var _url = '/api/User/saveRole?userId=' + this.role.row.User_Id;
      this.http.post(_url, checkedIds, '正在保存...').then((x) => {
        this.$Message[x.status ? 'info' : 'error'](x.message);
        this.role.model = false;
      });
    },
    open(row) {
      this.password = '';
      this.row = row;
      this.model = true;
    },
    savePwd() {
      if (!this.password) return this.$Message.error('请输密码');
      if (this.password.length < 6)
        return this.$Message.error('密码长度至少6位');
      let url =
        '/api/user/modifyUserPwd?password=' +
        this.password +
        '&userName=' +
        this.row.UserName;
      this.http.post(url, {}, true).then((x) => {
        if (!x.status) {
          return this.$message.error(x.message);
        }
        this.model = false;
        this.$Message.success(x.message);
      });
    }
  },
  created() {}
});
</script>
<style lang="less" scoped>
h3 {
  font-weight: 500;
  > span:last-child {
    margin-left: 30px;
  }
}
.action-group {
  width: 100%;
  display: flex;
  .action-text {
    flex: 1;
  }
}
.user_name {
  margin-bottom: 5px;
  font-weight: bold;
  padding-bottom: 7px;
  border-bottom: 1px solid #d7d7d7;
}
</style>
