<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/ltb/project/LTB_Project.js此处编写
 -->

<template>
 <div style="margin:10px 10px 5px ;width:820px;height:40px;float:right;">
  <el-input v-model="pdn" placeholder="SPN or PDN" clearable style="float:left;width:200px;margin-right:10px"></el-input>
  <el-button icon="el-icon-search"  size="small"  @click="GetProject()" >Search</el-button>
  <el-button type="primary" icon="el-icon-plus" size="small" @click="Add()">Add</el-button>
  <!-- <el-button type="success" icon="el-icon-edit" size="small" @click="Update()">Update</el-button> -->
  <el-button type="success" icon="el-icon-document-copy" size="small" @click="CopyProject()">Copy Project</el-button>
  <el-button type="success" icon="el-icon-document-copy" size="small" plain @click="CopyDemands()">Copy All</el-button>
  <el-button type="warning" icon="el-icon-tickets" size="small" @click="GoPDF()">View PDF</el-button>
  <el-button type="danger" icon="el-icon-delete" size="small" @click="Delete()">Delete</el-button>
</div>

  <el-table :data="tableData" @selection-change="handleSelectionChange" border style="width: 100%" height="750">
    <el-table-column type="selection" width="50"></el-table-column>
    <!-- <el-table-column prop="spn" label="SPN" width="105" /> -->
    <el-table-column  prop="spn" label="SPN" width="105">
        <template #default="scope">
            <el-button type="text" @click="spnDialogUpdate(scope.$index, scope.row)">{{scope.row.spn}}</el-button>
        </template>
    </el-table-column>
    
    <el-table-column prop="pdn" label="PDN" width="80" />
    <!-- <el-table-column prop="bl" label="BL" width="48"/> -->
    <el-table-column prop="obsoleteMaterialDescription" label="Description" width="412" />
    <!-- <el-table-column prop="affectedBLs" label="AffectedBLs" width="240" /> -->
    <el-table-column prop="manufacturer" label="Manufacturer" width="130" />
    <el-table-column prop="price" label="UnitPrice" width="100" />
    
    <el-table-column prop="stockUpStartdate" label="Stock Up Startdate" :formatter="formatDate" width="170" />
    
    <el-table-column prop="state" label="State" width="140" />
    <el-table-column label="Series  |  Service |  PDF"  width="415">
      <template #default="scope">
        <el-button size="mini"  plain  @click="GoSeriesDemands(scope.$index, scope.row)">Series</el-button>
        <el-button size="mini" type="primary"  plain  @click="GoServiceDemands(scope.$index, scope.row)">Service</el-button>
        <el-button size="mini" type="warning" plain  @click="GeneratePDF(scope.$index, scope.row)">Generate PDF</el-button>
      </template>
    </el-table-column>
  </el-table>

<!-- 弹出框  add/update -->
<div class="dialogContent">
 <el-dialog v-model="dialogFormVisible" :title="title">
    <el-form :model="form"  :inline="true" :rules="rules" ref="ruleForm">
      <el-form-item label="SPN" :label-width="formLabelWidth" prop="spn">
        <el-input v-model="form.spn" clearable autocomplete="off" :disabled="SPNdisabled" />
      </el-form-item>
      <el-form-item label="PDN" :label-width="formLabelWidth">
        <el-input v-model="form.pdn" clearable autocomplete="off" :disabled="PDNdisabled" />
      </el-form-item>
      <el-form-item label="BL" :label-width="formLabelWidth" style="color: red" prop="bl">
        <el-input v-model="form.bl" clearable autocomplete="off" :disabled="otherDisabled" />
      </el-form-item>
      <el-form-item label="Manufacturer" :label-width="formLabelWidth" prop="manufacturer">
        <el-input v-model="form.manufacturer" clearable autocomplete="off" :disabled="otherDisabled" />
      </el-form-item>
      <el-form-item label="Description" :label-width="formLabelWidth" style="width:725px;" prop="obsoleteMaterialDescription">
        <el-input type="textarea" v-model="form.obsoleteMaterialDescription" autocomplete="off" :disabled="otherDisabled" />
      </el-form-item>
      <el-form-item label="Affected BLs" :label-width="formLabelWidth" style="width:725px" prop="affectedBLs">
        <el-input type="textarea" v-model="form.affectedBLs" autocomplete="off" :disabled="otherDisabled" />
      </el-form-item>
        <el-form-item label="UnitPrice" :label-width="formLabelWidth" style="width:250px" prop="price">
        <el-input v-model="form.price" clearable autocomplete="off" :disabled="otherDisabled" />
      </el-form-item>
      <!-- 价格单位的下拉  有人民币、美元、欧元三种 :label-width="formLabelWidth"-->
      <el-form-item label="" style="width:98px">
        <el-select v-model="form.priceTypeID" :disabled="otherDisabled" placeholder="Unit">
          <el-option v-for="item in priceOptions" :key="item.value" :label="item.label" :value="item.value">
          </el-option>
        </el-select>
      </el-form-item>

       <el-form-item label="Stock up start date" :label-width="formLabelWidth">
        <el-date-picker v-model="form.stockUpStartdate" autocomplete="off" :disabled="StockUpStartdatedisabled" />
      </el-form-item>
      <!-- 远程搜索 DisplayName 后续做 -->
       <el-form-item label="Createder" :label-width="formLabelWidth">
        <!-- <el-input v-model="form.createder" autocomplete="off" /> -->
        <el-autocomplete clearable v-model="form.createder" :fetch-suggestions="querySearchAsync" placeholder="" @select="handleSelect" >    
        </el-autocomplete>
      </el-form-item>
       <el-form-item label="CreatedDate" :label-width="formLabelWidth">
        <el-date-picker v-model="form.createDate" autocomplete="off"  />
      </el-form-item>
       <el-form-item label="Approval1" :label-width="formLabelWidth">
        <!-- <el-input v-model="form.approvaler1" autocomplete="off" /> -->
        <el-autocomplete clearable v-model="form.approvaler1" :fetch-suggestions="querySearchAsync" placeholder="" @select="handleSelect" >    
        </el-autocomplete>
      </el-form-item>
       <el-form-item label="ApprovalDate1" :label-width="formLabelWidth">
        <el-date-picker v-model="form.approvalDate1" autocomplete="off"  />
      </el-form-item>
       <el-form-item label="Approval2" :label-width="formLabelWidth">
        <!-- <el-input v-model="form.approvaler2" autocomplete="off" /> -->
        <el-autocomplete clearable v-model="form.approvaler2" :fetch-suggestions="querySearchAsync" placeholder="" @select="handleSelect" >    
        </el-autocomplete>
      </el-form-item>
       <el-form-item label="ApprovalDate2" :label-width="formLabelWidth">
        <el-date-picker v-model="form.approvalDate2" autocomplete="off"  />
      </el-form-item>
      <el-form-item label="Approval3" :label-width="formLabelWidth">
        <!-- <el-input v-model="form.approvaler1" autocomplete="off" /> -->
        <el-autocomplete clearable v-model="form.approvaler3" :fetch-suggestions="querySearchAsync" placeholder="" @select="handleSelect" >    
        </el-autocomplete>
      </el-form-item>
       <el-form-item label="ApprovalDate3" :label-width="formLabelWidth">
        <el-date-picker v-model="form.approvalDate3" autocomplete="off"  />
      </el-form-item>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="dialogFormVisible = false">Cancel</el-button>
        <el-button type="primary" @click="AddOrUpdateProject('ruleForm')">
          Confirm
        </el-button>
      </span>
    </template>
  </el-dialog>
</div>

</template>


<script>
import {useRouter,useRoute} from 'vue-router'

export default {
  data() {
    return {
      tableData : [],
      searchInfo: {
        userName: '',
        passWord: '',
      },
      priceOptions:[{
          value: 1,
          label: '￥'
        }, {
          value: 2,
          label: '$'
        }, {
          value: 3,
          label: '€'
        }],
      pdn:'',
      dialogFormVisible : false,
      formLabelWidth : '140px',
      selectedProject:[],
      selectedProjectCount:0,
      PDNdisabled:false,
      SPNdisabled:false,
      StockUpStartdatedisabled:false,
      otherDisabled:false, //其它输入框默认是启用的，当Copy Project 时都禁用
      title:'Project -- Add',
      rules: { //校验规则
          spn: [
            { required: true, message: 'Please enter SPN', trigger: 'blur' }
          ],
          bl: [
            { required: true, message: 'Please enter BL', trigger: 'blur' }
          ],
          manufacturer: [
            { required: true, message: 'Please enter Manufacturer', trigger: 'blur' }
          ],
          obsoleteMaterialDescription: [
            { required: true, message: 'Please enter Description', trigger: 'blur' }
          ],
          affectedBLs: [
            { required: true, message: 'Please enter Affected BLs', trigger: 'blur' }
          ],
          price: [
            { required: true, message: 'Please enter Price', trigger: 'blur' }
          ],
      },
      form :{
        id:'', //project的id
        pdn: '',
        spn: '',
        bl: '',
        obsoleteMaterialDescription: '',
        affectedBLs: '',
        manufacturer: '',
        stockUpStartdate: '',
        price: '',
        priceTypeID:'',
        createder:'',
        createDate:'',
        approvaler1:'',
        approvalDate1:'',
        approvaler2:'',
        approvalDate2:'',
        approvaler3:'',
        approvalDate3:'',
      },
      //远程搜索employee
      restaurants: [], //返回的
      timeout:  null,
      state:'',

    };
  },
  created() {
    
  },
  mounted(){
      this.GetProject();
      this.getEmployee();
  },
  methods: {
    GetProject() {
        this.http.post('/api/LTB_Project/getProject?pdn='+this.pdn,"", 'loading....')
        .then((result) => {
          //console.log("GetProject",result);
          result.forEach(value => {
            if(value.priceTypeID==1 || value.priceTypeID=="1"){
              value.price=value.price+" ￥";
            }
            else if(value.priceTypeID==2 || value.priceTypeID=="2"){
              value.price=value.price+" $";
            }
            else if(value.priceTypeID==3 || value.priceTypeID=="3"){
              value.price=value.price+" €";
            }
          });
          //console.log("GetProject加上价格单位",result);
          this.tableData=result;
        });
    },
    AddOrUpdateProject(formName){
      this.$refs[formName].validate((valid) => {
          if (valid) {
            //console.log("AddOrUpdate 转化前",this.form)
            //添加是id=""  要给他赋值为0  不然后台转成实体类报错。。。
            if(this.form.id==""){
              this.form.id=0;
            }
            this.form.stockUpStartdate=this.transformDate(this.form.stockUpStartdate);
            this.form.createDate=this.transformDate(this.form.createDate);
            this.form.approvalDate1=this.transformDate(this.form.approvalDate1);
            this.form.approvalDate2=this.transformDate(this.form.approvalDate2);
            this.form.approvalDate3=this.transformDate(this.form.approvalDate3);
            console.log("AddOrUpdateProject 转化后",this.form)
            let jsonData = JSON.stringify(this.form);
            console.log("要编辑的数据转成json字符串：",jsonData);
            //判断此时是否是 Copy All
            if(this.title == "Project -- Copy All"){
              this.http.post('/api/LTB_Project/copyDemands?projectDataInfo='+jsonData,"", 'loading....')
              .then((result) => {
                  console.log("copyDemands",result);
                  if(result==-1){
                    this.$message({
                      message: 'The selected project does not have series & service demand value !',
                      type: 'warning'
                    });
                  }else{
                    this.$message({
                      message: 'Replicating Success ~',
                      type: 'success'
                    });
                    this.GetProject(); //刷新页面
                  }
              });
            }
            else{
              this.http.post('/api/LTB_Project/addOrUpdateProject?projectDataInfo='+jsonData+"&type="+this.title,'', 'loading....')
              .then((result) => {
                //console.log(result);
                this.dialogFormVisible=false;
                this.GetProject(); //刷新页面
              });
            }
          } 
          else 
          {
            console.log('error submit!!');
            return false;
          }
      }); 
    },
    Add(){
        this.dialogFormVisible = true;
        this.title="Project -- Add";
        //清空数据
        this.form.id="";
        this.form.pdn='';
        this.form.spn='';
        this.form.bl='';
        this.form.obsoleteMaterialDescription='';
        this.form.affectedBLs='';
        this.form.manufacturer='';
        this.form.stockUpStartdate='';
        this.form.price='';
        this.form.createder='';
        this.form.createDate='';
        this.form.approvaler1='';
        this.form.approvalDate1='';
        this.form.approvaler2='';
        this.form.approvalDate2='';
        this.form.approvaler3='';
        this.form.approvalDate3='';
        //启用PDN输入框  
        this.PDNdisabled=false;
        this.SPNdisabled=false;  
        this.StockUpStartdatedisabled=false;  
    },
    Update(){
        if(this.selectedProjectCount==0){
          this.$message({
            message: 'Please select a piece of data to edit',
            type: 'warning'
          });
        }
        else if(this.selectedProjectCount>1){ 
          this.$message({
            message: 'Only one piece of data can be selected for editing',
            type: 'warning'
          });
        }
        else if(this.selectedProjectCount==1){
            console.log("Update",this.selectedProject,this.selectedProjectCount);
            //打开弹出框 
            this.dialogFormVisible=true;
            this.title="Project -- Update";
            //并对其进行数据反填
            this.form.id=this.selectedProject[0].id;
            this.form.pdn=this.selectedProject[0].pdn;
            this.form.spn=this.selectedProject[0].spn;
            this.form.bl=this.selectedProject[0].bl;
            this.form.obsoleteMaterialDescription=this.selectedProject[0].obsoleteMaterialDescription;
            this.form.affectedBLs=this.selectedProject[0].affectedBLs;
            this.form.manufacturer=this.selectedProject[0].manufacturer;
            this.form.stockUpStartdate=this.selectedProject[0].stockUpStartdate=="1900-01-01 00:00:00"?"":this.selectedProject[0].stockUpStartdate;
            // this.form.price=this.selectedProject[0].price;
            this.form.price=this.selectedProject[0].price.replace("$",'').replace("￥",'').replace("€",'');
            this.form.priceTypeID=this.selectedProject[0].priceTypeID;
            this.form.createder=this.selectedProject[0].createder;
            this.form.createDate=this.selectedProject[0].createDate=="1900-01-01 00:00:00"?"":this.selectedProject[0].createDate;
            this.form.approvaler1=this.selectedProject[0].approvaler1;
            this.form.approvalDate1=this.selectedProject[0].approvalDate1=="1900-01-01 00:00:00"?"":this.selectedProject[0].approvalDate1;
            this.form.approvaler2=this.selectedProject[0].approvaler2;
            this.form.approvalDate2=this.selectedProject[0].approvalDate2=="1900-01-01 00:00:00"?"":this.selectedProject[0].approvalDate2;
            this.form.approvaler3=this.selectedProject[0].approvaler3;
            this.form.approvalDate3=this.selectedProject[0].approvalDate3=="1900-01-01 00:00:00"?"":this.selectedProject[0].approvalDate3;
            
            //禁用PDN输入框
            //this.PDNdisabled=true;
            //this.SPNdisabled=true;
            //this.StockUpStartdatedisabled=true;
            //启用其他输入框
            this.otherDisabled=false;
        }
    },
    //点击spn弹出update框
    spnDialogUpdate(index, row){
        //打开弹出框 
        this.dialogFormVisible=true;
        this.title="Project -- Update";
        //并对其进行数据反填
        this.form.id=row.id;
        this.form.pdn=row.pdn;
        this.form.spn=row.spn;
        this.form.bl=row.bl;
        this.form.obsoleteMaterialDescription=row.obsoleteMaterialDescription;
        this.form.affectedBLs=row.affectedBLs;
        this.form.manufacturer=row.manufacturer;
        this.form.stockUpStartdate=row.stockUpStartdate=="1900-01-01 00:00:00"?"":row.stockUpStartdate;
        // this.form.price=this.selectedProject[0].price;
        this.form.price=row.price.replace("$",'').replace("￥",'').replace("€",'');
        this.form.priceTypeID=row.priceTypeID;
        this.form.createder=row.createder;
        this.form.createDate=row.createDate=="1900-01-01 00:00:00"?"":row.createDate;
        this.form.approvaler1=row.approvaler1;
        this.form.approvalDate1=row.approvalDate1=="1900-01-01 00:00:00"?"":row.approvalDate1;
        this.form.approvaler2=row.approvaler2;
        this.form.approvalDate2=row.approvalDate2=="1900-01-01 00:00:00"?"":row.approvalDate2;
        this.form.approvaler3=row.approvaler3;
        this.form.approvalDate3=row.approvalDate3=="1900-01-01 00:00:00"?"":row.approvalDate3;

        //启用其他输入框
        this.otherDisabled=false;    

    },
    CopyProject(){
      if(this.selectedProjectCount==0){
          this.$message({
            message: 'Please select a piece of data to copy',
            type: 'warning'
          });
        }
        else if(this.selectedProjectCount>1){ 
          this.$message({
            message: 'Only one piece of data can be selected for copying',
            type: 'warning'
          });
        }
        else if(this.selectedProjectCount==1){
            console.log("CopyProject",this.selectedProject,this.selectedProjectCount);
            //打开弹出框 
            this.dialogFormVisible=true;
            this.title="Project -- Copy Project";
            //并对其进行数据反填
            this.form.id=this.selectedProject[0].id;
            this.form.pdn=this.selectedProject[0].pdn;
            this.form.spn=this.selectedProject[0].spn;
            this.form.bl=this.selectedProject[0].bl;
            this.form.obsoleteMaterialDescription=this.selectedProject[0].obsoleteMaterialDescription;
            this.form.affectedBLs=this.selectedProject[0].affectedBLs;
            this.form.manufacturer=this.selectedProject[0].manufacturer;
            this.form.stockUpStartdate=this.selectedProject[0].stockUpStartdate=="1900-01-01 00:00:00"?"":this.selectedProject[0].stockUpStartdate;
            // this.form.price=this.selectedProject[0].price;
            this.form.price=this.selectedProject[0].price.replace("$",'').replace("￥",'').replace("€",'');
            this.form.priceTypeID=this.selectedProject[0].priceTypeID;

            //创建人和批准人要重新设置
            this.form.stockUpStartdate="";
            this.form.createder="";
            this.form.createDate="";
            this.form.approvaler1="";
            this.form.approvalDate1="";
            this.form.approvaler2="";
            this.form.approvalDate2="";
            this.form.approvaler3="";
            this.form.approvalDate3="";
            
            //除了 Stock up start date 和 创建人、批准人  其他都禁用
            this.PDNdisabled=true;
            this.SPNdisabled=true;
            this.otherDisabled=true;
            //启用StockUpStartdate 输入框
            this.StockUpStartdatedisabled=false;
        }

    },
    Delete(){
        if(this.selectedProjectCount==0){
          this.$message({
            message: 'Please select a piece of data to delete',
            type: 'warning'
          });
        }else{
          this.$confirm('Are you sure you want to delete it ?', '', {
          confirmButtonText: 'Conform',
          cancelButtonText: 'Cancle',
          type: 'warning'
          }).then(() => {
             let delPDN=[];
             let delPorjectID=[];
            //删除 单个或多个
            this.selectedProject.forEach(p => {
                delPDN.push(p.pdn);   //=this.selectedProject[0].pdn;  
                delPorjectID.push(p.id)
            });
            let pdn = JSON.stringify(delPDN);
            let pIds = JSON.stringify(delPorjectID);
            //console.log(pdn);
            this.http.post('/api/LTB_Project/deleteProject?pIds='+pIds,'', 'loading....')
           .then((result) => {
             this.GetProject(); //刷新页面
            });
          }).catch(() => {
          this.$message({
            type: 'info',
            message: 'Cancelled deletion'
          });          
         });
            
        }
    },
    handleSelectionChange(val) {
        //console.log("handleSelectionChange",val.length);
        //console.log(val[0].pdn);
       this.selectedProject = val;
       this.selectedProjectCount=val.length;
    },
    //替换表格时间格式 ==> add和update时 时间会变成标准时间格式 还得转化
    formatDate(row, column) {
        // 获取单元格数据
        let date = row[column.property];
        //console.log(date,column);
        if (date) {
            if(date=='1900-01-01 00:00:00'){
                return ''
            }
            return this.transformDate(date)
        } else {
             return ''
        }
    },
    //标准时间格式转yyyy-MM-dd HH:mm:ss
     transformDate(date) {
          if (date) {
            let dt = new Date(date);
            return dt.getFullYear() + '-' +
                  ((dt.getMonth() + 1) < 10 ? ('0' + (dt.getMonth() + 1)) : (dt.getMonth() + 1)) + '-' +
                  (dt.getDate() < 10 ? ('0' + dt.getDate()) : dt.getDate()) // + ' ' +
                  //(dt.getHours() < 10 ? ('0' + dt.getHours()) : dt.getHours()) + ':' +
                 // (dt.getMinutes() < 10 ? ('0' + dt.getMinutes()) : dt.getMinutes()) + ':' +
                 //(dt.getSeconds() < 10 ? ('0' + dt.getSeconds()) : dt.getSeconds())
         } else {
            return ''
        }
     },

     //远程搜索employee
     querySearchAsync(queryString, cb) {
        var restaurants = this.restaurants;
        var results = queryString ? restaurants.filter(this.createStateFilter(queryString)) : restaurants;

        clearTimeout(this.timeout);
        this.timeout = setTimeout(() => {
          cb(results);
        }, 1000 * Math.random());
      },
      createStateFilter(queryString) {
        return (state) => {
          return (state.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
        };
      },
      //获取所有的employee
      getEmployee(){
        this.http.post('/api/LTB_Project/getEmployee',"", 'loading....')
        .then((result) => {
            //console.log("getEmployee",result);
            this.restaurants=result;
        });
      },
      

    GoSeriesDemands(index, row) {
        console.log("跳转SeriesDemands",index, row.spn,row.pdn,row.id);
        //重新设置存在vuex的参数
        // localStorage.setItem('seriesSpn',row.spn);
        // localStorage.setItem('seriesPdn',row.pdn);
        // localStorage.setItem('seriesProjectId',row.id);

        this.$router.push({ 
           name:'LTB_SeriesDemands',
           params: {  
               spn:row.spn,
               pdn:row.pdn,
               projectId:row.id,
           }
        }); 
    },
    GoServiceDemands(index, row) {
        console.log("跳转ServiceDemands",index, row.spn,row.pdn,row.id);
        this.$router.push({ 
           name:'LTB_ServiceDemands',
           params: {  
               spn:row.spn,
               pdn:row.pdn,
               projectId:row.id,
           }
        }); 
    },
    //copy 某一project的 全部信息 包括 series & service demand
    CopyDemands(){
      if(this.selectedProjectCount==0){
          this.$message({
            message: 'Please select a piece of data to Copy Demands',
            type: 'warning'
          });
        }
        else if(this.selectedProjectCount>1){ 
          this.$message({
            message: 'Only one piece of data can be selected for replication',
            type: 'warning'
          });
        }
        else if(this.selectedProjectCount==1){
          //打开弹出框 
            this.dialogFormVisible=true;
            this.title="Project -- Copy All";
            //并对其进行数据反填
            this.form.id=this.selectedProject[0].id;
            this.form.pdn='';
            this.form.spn='';
            this.form.bl='';
            this.form.obsoleteMaterialDescription='';
            this.form.affectedBLs='';
            this.form.manufacturer='';
            this.form.price='';
            this.form.priceTypeID=this.selectedProject[0].priceTypeID;
            this.form.stockUpStartdate=this.selectedProject[0].stockUpStartdate=="1900-01-01 00:00:00"?"":this.selectedProject[0].stockUpStartdate;            
            this.form.stockUpStartdate=this.selectedProject[0].stockUpStartdate=="1900-01-01 00:00:00"?"":this.selectedProject[0].stockUpStartdate;           
            this.form.createder=this.selectedProject[0].createder;
            this.form.createDate=this.selectedProject[0].createDate=="1900-01-01 00:00:00"?"":this.selectedProject[0].createDate;
            this.form.approvaler1=this.selectedProject[0].approvaler1;
            this.form.approvalDate1=this.selectedProject[0].approvalDate1=="1900-01-01 00:00:00"?"":this.selectedProject[0].approvalDate1;
            this.form.approvaler2=this.selectedProject[0].approvaler2;
            this.form.approvalDate2=this.selectedProject[0].approvalDate2=="1900-01-01 00:00:00"?"":this.selectedProject[0].approvalDate2;
            this.form.approvaler3=this.selectedProject[0].approvaler3;
            this.form.approvalDate3=this.selectedProject[0].approvalDate3=="1900-01-01 00:00:00"?"":this.selectedProject[0].approvalDate3;
            
            //除了 Stock up start date 和 创建人、批准人  其他都禁用
            this.PDNdisabled=false;
            this.SPNdisabled=false;
            this.otherDisabled=false;
            //启用StockUpStartdate 输入框
            this.StockUpStartdatedisabled=false;
            
        }

    },
    
    //去往展示PDF页
      GoPDF(){
        debugger
        if(this.selectedProjectCount==0){
            this.$message({
              message: 'Please select a piece of data to display as a PDF',
              type: 'warning'
            });
          }
          else if(this.selectedProjectCount>1){ 
            this.$message({
              message: 'Only one piece of data can be selected for PDF display',
              type: 'warning'
            });
          }else if(this.selectedProjectCount==1){
            debugger
            this.http.post('/api/LTB_Project/GeneratePDF?Id='+this.selectedProject[0].id,"", 'loading....')
              .then((result) => {
                debugger
                console.log("GeneratePDF",result);
                 if(result){                
                     window.open("/"+result.split('_/')[1],"_blank");
                   }
              });
          }
      },
    GeneratePDF(index, row){
      if(row.state=="Service Done"){
        this.$confirm('At this time, only Service Done is available. Are you sure you want to generate a PDF?', '', {
          confirmButtonText: 'Generate',
          cancelButtonText: 'Cancel',
          type: 'warning'
        }).then(() => {
          this.http.post('/api/LTB_Project/GeneratePDF?Id='+row.id,"", 'loading....')
          .then((result) => {
             //console.log("GeneratePDF",result);
             if(result){                
               window.open("/"+result.split('_/')[1],"_blank");
               this.$message({
                 type: 'success',
                 message: 'Successfully generated!'
               });
             }
           });
        }).catch(() => {
          this.$message({
            type: 'info',
            message: 'Generation canceled'
          });          
        });
      }
      else if(row.state=="Series Done"){
        this.$confirm('At this time, only Series Done is available. Are you sure you want to generate a PDF?', '', {
          confirmButtonText: 'Generate',
          cancelButtonText: 'Cancel',
          type: 'warning'
        }).then(() => {
          this.http.post('/api/LTB_Project/GeneratePDF?Id='+row.id,"", 'loading....')
          .then((result) => {
             //console.log("GeneratePDF",result);
             if(result){                
               window.open("/"+result.split('_/')[1],"_blank");
               this.$message({
                 type: 'success',
                 message: 'Successfully generated!'
               });
             }
           });
        }).catch(() => {
          this.$message({
            type: 'info',
            message: 'Generation canceled'
          });          
        });
      }
      else if(row.state=="New"){
        this.$message({
            type: 'info',
            message: 'At this time, the status is new and cannot generate a PDF!'
        }); 
      }else{
        this.http.post('/api/LTB_Project/GeneratePDF?Id='+row.id,"", 'loading....')
        .then((result) => {
          console.log("GeneratePDF",result);
          if(result){                
            window.open("/"+result.split('_/')[1],"_blank");
          }
        });
      }
    },
   
  },
//   // 监听,当路由发生变化的时候执行
  watch:{
    $route(to,from){
      this.GetProject(); //刷新页面
    }
  },
  
  }

</script>

<style scoped lang='less'>
.dialogContent{
    :deep(.el-dialog) {
        .el-form .el-form-item .el-input{
            font-family: "Helvetica Neue",Helvetica,"PingFang SC","Hiragino Sans GB","Microsoft YaHei","微软雅黑",Arial,sans-serif;
            color:red;
        }
    } 
}
</style>

 <!-- 修改elementui里边对话框的默认样式
     需要注意两点：
     1：样式声明的时候不要加scoped，不然样式只在当前组件起作用，无法覆盖样式内容的样式
     2：要修改默认弹窗里边的样式，加一个前缀，就可以做到只修改当前这个组件的了，不然可能会影响到其他地方的样式
-->
<style lang='less'>
/*  修改el-dialog里边的默认样式 */
     .dialogContent{
         .el-input{
             font-family: "Helvetica Neue",Helvetica,"PingFang SC","Hiragino Sans GB","Microsoft YaHei","微软雅黑",Arial,sans-serif;
            color:red;
         }

     }
</style>  