<template>
<div style="margin:10px 10px 5px;width:730px;height:40px;float:right">
  <el-input v-model="material" placeholder="Counter Material" clearable style="float:left;width:200px;margin-right:10px"></el-input>
  <el-button icon="el-icon-search" size="small" clearable @click="GetSeriesCommon()" >Search</el-button>
  <el-button type="primary" icon="el-icon-plus" size="small" @click="MaterialDialog()">Add Material</el-button>
  <el-button type="primary" icon="el-icon-plus" size="small" @click="AddSeriesDemands()">Add</el-button>
  <el-button type="success" icon="el-icon-check" size="small" @click="ChangeProjectState()" >Series Done</el-button>  
  <el-button type="warning" icon="el-icon-back" size="small" @click="BackGoProject()" >Back</el-button>
 
  <!-- 模拟projectName的多选下拉 -->
  <!-- <div class="block">
    <el-cascader
    :options="options"
    :props="{ multiple: true, expandTrigger: 'hover' }"
    :show-all-levels="false"
    clearable>
    </el-cascader>
  </div> -->
</div>

  <el-descriptions class="margin-top" :column="4" :size="size" border>  
     <el-descriptions-item>
      <template #label>
        <div class="cell-item">BL</div>
      </template>
      {{pBL}}
    </el-descriptions-item>
     <el-descriptions-item>
      <template #label>
        <div class="cell-item">PDN</div>
      </template>
      {{pPDN}}
    </el-descriptions-item>
      <el-descriptions-item>
      <template #label>
        <div class="cell-item">SPN</div>
      </template>
      {{pSPN}}
    </el-descriptions-item>
      <el-descriptions-item>
      <template #label>
        <div class="cell-item">Description obsolete material</div>
      </template>
      {{pDescription}}
    </el-descriptions-item>
  </el-descriptions>

  <br/>

  <el-table :data="seriesCommonData" border style="width: 100%" @row-dblclick="RowDblclick" height="650">
    <el-table-column label="Obsolescence Tool">  
        <el-table-column  prop="counterMaterial" label="Counter Material" width="133">
            <template #default="scope">
                <span v-if="scope.row.isEdit">{{scope.row.counterMaterial}}</span>
                 <el-input v-else v-model="scope.row.counterMaterial"></el-input> <!--:disabled="counterMaterialDisabled" -->
            </template>
        </el-table-column>
        <!-- <el-table-column prop="counterMaterial" label="Counter Material" width="87" /> -->

        <el-table-column  prop="counterMaterialDescription" label="Description Counter Material" width="160">
            <template #default="scope">
                <span v-if="scope.row.isEdit">{{scope.row.counterMaterialDescription}}</span>
                <el-input v-else  v-model="scope.row.counterMaterialDescription"></el-input>
            </template>
        </el-table-column>
        <!-- <el-table-column prop="counterMaterialDescription" label="Description Counter Material" width="100"/> -->
        
        <!-- <el-table-column  prop="Plant" label="plant" width="110">
            <template #default="scope">
                <span v-if="scope.row.isEdit">{{scope.row.plant}}</span>
                <el-select v-else v-model="scope.row.plant" placeholder="">
                   <el-option v-for="item in plantOptions" :key="item.id" :label="item.plant" :value="item.id">
                   </el-option>
                </el-select>
            </template>
        </el-table-column> -->
        <el-table-column  prop="Plant" label="plant" width="110">
            <template #default="scope">
                <span v-if="scope.row.isEdit">{{scope.row.plant}}</span>
                <el-select v-else v-model="scope.row.plantID" placeholder="">
                   <el-option v-for="item in plantOptions" :key="item.id" :label="item.plant" :value="item.id">
                   </el-option>
                </el-select>
            </template>
        </el-table-column>
        <!-- <el-table-column prop="plant" label="Plant" width="56" /> -->

        <!-- <el-table-column  prop="sendingPlant" label="Sending Plant" width="118">
            <template #default="scope">
                <span v-if="scope.row.isEdit">{{scope.row.sendingPlant}}</span>
                <el-select v-else v-model="scope.row.sendingPlant" placeholder="">
                   <el-option v-for="item in sendingPlantOptions" :key="item.id" :label="item.sendingPlant" :value="item.id">
                   </el-option>
                </el-select>   
            </template>
        </el-table-column> -->
        <el-table-column  prop="sendingPlant" label="Sending Plant" width="118">
            <template #default="scope">
                <span v-if="scope.row.isEdit">{{scope.row.sendingPlant}}</span>
                <el-select v-else v-model="scope.row.sendingPlantID" placeholder="">
                   <el-option v-for="item in sendingPlantOptions" :key="item.id" :label="item.sendingPlant" :value="item.id">
                   </el-option>
                </el-select>   
            </template>
        </el-table-column>
        <!-- <el-table-column prop="sendingPlant" label="Sending Plant" width="78"/> -->

        <el-table-column  prop="customer" label="Customer" width="110">
            <template #default="scope">
                <span v-if="scope.row.isEdit">{{scope.row.customer}}</span>
                <el-input v-else  v-model="scope.row.customer"></el-input>
            </template>
        </el-table-column>
        <!-- <el-table-column prop="customer" label="Customer" width="88" /> -->
        <el-table-column  prop="counterType" label="Counter Type" width="170">
            <template #default="scope">
                <span v-if="scope.row.isEdit">{{scope.row.counterType}}</span>
                <el-select v-else v-model="scope.row.counterTypeID" placeholder="">
                   <el-option v-for="item in counterTypeOptions" :key="item.id" :label="item.counterType" :value="item.id">
                   </el-option>
                </el-select>
                <!-- <el-input v-else  v-model="scope.row.counterType"></el-input> -->
                <!-- <el-select v-else v-model="scope.row.CounterTypeID" placeholder="">
                  <el-option label="direct delivery" value="1" />
                  <el-option label="indirect delivery" value="2" />
                  <el-option label="manufacturing order supplier" value="3" />
                  <el-option label="consumption BU" value="4" />
                  <el-option label="flat-rate allocation" value="5" />
                  <el-option label="stock transfer" value="6" />
                </el-select> -->
            </template>
        </el-table-column>
        <!-- <el-table-column prop="counterType" label="Counter Type" width="88" /> -->
        <el-table-column  prop="onStockFrom" label="on stock form" width="165">
            <template #default="scope">
                <span v-if="scope.row.isEdit">{{scope.row.onStockFrom}}</span>
                <!-- <el-input v-else  v-model="scope.row.onStockFrom"></el-input> -->
                <el-date-picker v-else v-model="scope.row.onStockFrom" autocomplete="off" style="width:140px" />
            </template>
        </el-table-column>
        <!-- <el-table-column prop="onStockFrom" label="on stock form" :formatter="formatDate" width="80"/> -->
        <el-table-column  prop="onStockUntil" label="on stock until" width="165">
            <template #default="scope">
                <span v-if="scope.row.isEdit">{{scope.row.onStockUntil}}</span>
                <!-- <el-input v-else  v-model="scope.row.onStockUntil"></el-input> -->
                <el-date-picker v-else v-model="scope.row.onStockUntil" autocomplete="off" style="width:140px" />
            </template>
        </el-table-column>
        <!-- <el-table-column prop="onStockUntil" label="on stock until" :formatter="formatDate" width="80"/> -->
        <el-table-column  prop="coverageType" label="Coverage Type" width="93">
            <template #default="scope">
                <span v-if="scope.row.isEdit">{{scope.row.coverageType}}</span>
                <!-- <el-input v-else  v-model="scope.row.coverageType"></el-input> -->
                <el-select v-else v-model="scope.row.coverageType" placeholder="">
                   <el-option label="R" value="1" />
                   <el-option label="Z" value="2" />
                   <!-- <el-option label="Last Time Buy (Resteindeckung)" value="1" />
                   <el-option label="Interim Stock up (Zwischeneindeckung)" value="2" /> -->
                </el-select>
            </template>
        </el-table-column>        
        <!-- <el-table-column prop="coverageType" label="Coverage type" width="87"/> -->
    </el-table-column>
    <el-table-column  prop="projectNames" label="Project" width="145">
        <template #default="scope">
            <!-- 第一版：多选级联下拉 -->
            <span v-if="scope.row.isEdit">{{scope.row.projectNames}}</span>
            <select-Project v-else v-bind:projectInfo="parentMsg"  :selected="selectedProject" :selectedID="selectedProjectID" @handleChange="gethandleChangeData"></select-Project>
            
            <!-- 第二版：多选下拉 -->
            <!-- <span v-if="scope.row.isEdit">{{scope.row.projectNames}}</span>
            <el-select v-else v-model="scope.row.projectNameID" multiple  placeholder=""> 
                <el-option v-for="item in projectNameDropdownOptions" :key="item.rowID" :label="item.projectName" :value="item.rowID">
                </el-option>
            </el-select>  -->
        </template>
    </el-table-column> 
    <!-- <el-table-column prop="projectNames" label="Project" width="70" /> -->

    <el-table-column label="Required quantity">
        <el-table-column label="Plan rest of current FY" width="99">
            <el-table-column  prop="planRestOfCurrentFY" :label="restFY" width="99">
               <template #default="scope">
                   <span v-if="scope.row.isEdit">{{scope.row.planRestOfCurrentFY}}</span>
                   <el-input v-else  v-model="scope.row.planRestOfCurrentFY"></el-input>
               </template>
           </el-table-column>
            <!-- <el-table-column prop="planRestOfCurrentFY" :label="restFY" width="99"/> -->
        </el-table-column>
        <el-table-column label="Plan 1st following FY" width="102">
            <el-table-column  prop="planFirstFollowingFY" :label="firstFY" width="102">
               <template #default="scope">
                   <span v-if="scope.row.isEdit">{{scope.row.planFirstFollowingFY}}</span>
                   <el-input v-else  v-model="scope.row.planFirstFollowingFY"></el-input>
               </template>
           </el-table-column>            
            <!-- <el-table-column prop="planFirstFollowingFY" :label="firstFY" width="95"/> -->
        </el-table-column>
        <el-table-column label="Plan 2nd following FY" width="99">
            <el-table-column  prop="planSecondFollowingFY" :label="secondFY" width="99">
               <template #default="scope">
                   <span v-if="scope.row.isEdit">{{scope.row.planSecondFollowingFY}}</span>
                   <el-input v-else  v-model="scope.row.planSecondFollowingFY"></el-input>
               </template>
           </el-table-column> 
            <!-- <el-table-column prop="planSecondFollowingFY" :label="secondFY" width="97"/> -->
        </el-table-column>
        <el-table-column label="Plan 3rd following FY" width="99">
            <el-table-column  prop="planThirdFollowingFY" :label="thirdFY" width="99">
               <template #default="scope">
                   <span v-if="scope.row.isEdit">{{scope.row.planThirdFollowingFY}}</span>
                   <el-input v-else  v-model="scope.row.planThirdFollowingFY"></el-input>
               </template>
           </el-table-column> 
            <!-- <el-table-column prop="planThirdFollowingFY" :label="thirdFY" width="95"/> -->
        </el-table-column>
        <el-table-column prop="sumPlan" label="Sum Plan" width="64"/>
    </el-table-column>   
   
        <el-table-column  prop="componentQuantity" label="Component quantity" width="100">
            <template #default="scope">
                <span v-if="scope.row.isEdit">{{scope.row.componentQuantity}}</span>
                 <el-input v-else v-model="scope.row.componentQuantity"></el-input>
            </template>
        </el-table-column> 
    <!-- <el-table-column prop="componentQuantity" label="Component quantity" width="100" /> -->
    <el-table-column prop="totalDemand" label="Total Demand" width="114" />
    <el-table-column label="Operate" width="170">
      <template #default="scope">
        <el-button size="mini" type="primary" plain  @click="UpdateSeriesDemands(scope.$index, scope.row)">Update</el-button>
        <el-button size="mini" type="danger" plain  @click="DeleteSeriesDemands(scope.$index, scope.row)">Delete</el-button>
      </template>
    </el-table-column>
  </el-table>

  
<el-descriptions class="margin-top" :column="4" :size="size" border>  
     <el-descriptions-item>
      <template #label>
        <div class="cell-item">Stock up start date</div>
      </template>
      {{pStockupstartdate}}
    </el-descriptions-item>
     <el-descriptions-item>
      <template #label>
        <div class="cell-item">Price</div>
      </template>
      {{pPrice}}
    </el-descriptions-item>
      <el-descriptions-item>
      <template #label>
        <div class="cell-item">Total Series Costs</div>
      </template>
      {{totalSeriesCosts}}
    </el-descriptions-item>
      <el-descriptions-item>
      <template #label>
        <div class="cell-item">Total Series Demand</div>
      </template>
      {{totalSeriesDemand}} pcs
    </el-descriptions-item>
</el-descriptions>

  <!-- 弹出框  add/update -->
<div class="dialogContent">
 <el-dialog v-model="dialogFormVisible" :title="title" width="60%">
    <el-form :model="form"  :inline="true">
      <el-form-item label="Counter Material" :label-width="formLabelWidth">
        <el-input v-model="form.counterMaterial" autocomplete="off" style="width:220px" />
      </el-form-item>
      <el-form-item label="Description Counter Material" :label-width="formLabelWidth">
        <el-input v-model="form.counterMaterialDescription" autocomplete="off" style="width:220px" />
      </el-form-item>
      <el-form-item label="Plant" :label-width="formLabelWidth" style="color: red">
        <el-input v-model="form.plant" autocomplete="off" style="width:220px" />
      </el-form-item>
      <el-form-item label="Sending Plant" :label-width="formLabelWidth">
        <el-input v-model="form.sendingPlant" autocomplete="off" style="width:220px" />
      </el-form-item>
      <el-form-item label="Customer" :label-width="formLabelWidth">
        <el-input v-model="form.customer" autocomplete="off" style="width:220px" />
      </el-form-item>
      <el-form-item label="Project" :label-width="formLabelWidth">
        <el-input v-model="form.projectName" autocomplete="off" style="width:220px" />
      </el-form-item>
       <!-- 下拉 -->
      <el-form-item label="Counter Type" :label-width="formLabelWidth">
        <el-select v-model="form.counterType" placeholder="" style="width:220px">
          <el-option label="direct delivery" value="1" />
          <el-option label="indirect delivery" value="2" />
          <el-option label="manufacturing order supplier" value="3" />
          <el-option label="consumption BU" value="4" />
          <el-option label="flat-rate allocation" value="5" />
          <el-option label="stock transfer" value="6" />
        </el-select>
      </el-form-item>
      <el-form-item label="Coverage Type" :label-width="formLabelWidth" >
        <el-select v-model="form.coverageType" placeholder="">
          <el-option label="R" value="1" />
          <el-option label="Z" value="2" />
          <!-- <el-option label="Last Time Buy (Resteindeckung)" value="1" />
          <el-option label="Interim Stock up (Zwischeneindeckung)" value="2" /> -->
        </el-select>
      </el-form-item>

        <el-form-item label="On Stock From" :label-width="formLabelWidth">
           <el-date-picker v-model="form.onStockFrom" autocomplete="off" />
        </el-form-item>
        <el-form-item label="On Stock Until" :label-width="formLabelWidth">
           <el-date-picker v-model="form.onStockUntil" autocomplete="off" />
        </el-form-item>

      
      <el-form-item label="Plan rest of current FY" :label-width="formLabelWidth">
        <el-input v-model="form.planRestOfCurrentFY" autocomplete="off" style="width:220px" />
      </el-form-item>
      <el-form-item label="Plan 1st following FY" :label-width="formLabelWidth">
        <el-input v-model="form.planFirstFollowingFY" autocomplete="off" style="width:220px" />
      </el-form-item>
      <el-form-item label="Plan 2nd following FY" :label-width="formLabelWidth">
        <el-input v-model="form.planSecondFollowingFY" autocomplete="off" style="width:220px" />
      </el-form-item>
      <el-form-item label="Plan 3rd following FY" :label-width="formLabelWidth">
        <el-input v-model="form.planThirdFollowingFY" autocomplete="off" style="width:220px" />
      </el-form-item>
      <el-form-item label="Component Quantity" :label-width="formLabelWidth">
        <el-input v-model="form.componentQuantity" autocomplete="off" style="width:220px" />
      </el-form-item>
       <el-form-item label="Sum Plan :" :label-width="150">
        <!-- <el-input v-model="form.sumPlan" autocomplete="off" :disabled="false" /> -->
        {{realtimeSumPlan}}  
      </el-form-item>
       <el-form-item label="Total Demand :" :label-width="150">
        <!-- <el-input v-model="form.totalDemand" autocomplete="off" :disabled="false" /> -->
        {{realtimeTotalDemand}}
      </el-form-item>

    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="dialogFormVisible = false">Cancel</el-button>
        <el-button type="primary" @click="AddOrUpdateSeriesDemands()">
          Confirm
        </el-button>
      </span>
    </template>
  </el-dialog>
</div>

<!-- 弹出框 富文本框 -->
<div class="dialogContent">
  <el-dialog v-model="dialogMaterial" title="Material & Description" width="60%">  
      <Myeditor @undataMaterial="DialogAddMaterial" />
  </el-dialog>
</div>


</template>


<script>
import {useRouter,useRoute} from 'vue-router'
//级联多选下拉
import Select from './Select'
//富文本编辑器
import Myeditor from './Myeditor'


export default {
  components: {
    'selectProject':Select, //注册组件，名字用驼峰式
    Myeditor,
  },
  data() {
    return {
      parentMsg:[], //所有的project的值 ，传给select子组件
      selectedProject:'',//project多选下拉中选中的值（update前），传给子组件
      selectedProjectID:'',//project多选下拉中选中的值对应的ID（update前），传给子组件
      changedSelectedProject:'', //加或减project后，子组件传来给父组件的值
      seriesCommonData : [],
      plantOptions:[],
      sendingPlantOptions:[],
      counterTypeOptions:[],
      projectNamesOptions:[],
      projectNameDropdownOptions:[],
      spn:'',
      pdn:'',
      projectId:'',
      material:'',
      formLabelWidth:200,
      totalSeriesCosts:'',
      totalSeriesDemand:'',
      restFY:'',
      firstFY:'',
      secondFY:'',
      thirdFY:'',
      dialogFormVisible : false,
      dialogMaterial: false,
      counterMaterialDisabled:true,
      //project信息
      pBL:'',
      pPDN:'',
      pSPN:'',
      pDescription:'',
      pStockupstartdate:'',
      pPrice:'',
      //add、update form
      form:{
          id:'',
          projectId:'',
          counterMaterial:'',
          counterMaterialDescription:'',
          plant:'',
          sendingPlant:'',
          customer:'',
          counterType:'',
          coverageType:'',
          onStockFrom:'',
          onStockUntil:'',
          projectName:'',
          planRestOfCurrentFY:'',
          planFirstFollowingFY:'',
          planSecondFollowingFY:'',
          planThirdFollowingFY:'',
          sumPlan:'',
          componentQuantity:'',
          totalDemand:'',
          ssCommonID:'',
      },
      //realtimeSumPlan:'', //实时计算四个FollowingFY的和 ==> this.form.sumPlan
      //realtimeTotalDemand:'',//实时计算 realtimeSumPlan*componentQuantity ==> this.form.totalDemand
      //富文本编辑器
      MaterialTextarea:'', 
    };
  },
  mounted() {
    //params传参
    this.spn=this.$route.params.spn; //localStorage.getItem('seriesSpn'); 
    this.pdn=this.$route.params.pdn; //localStorage.getItem('seriesPdn'); 
    this.projectId=this.$route.params.projectId;  //localStorage.getItem('seriesProjectId');
    console.log("params传参",this.spn,this.pdn,this.projectId);

    this.AutoFY();
    this.material=""; //清空要查询的counterMaterial
    this.GetProject();

    //下拉绑定
    this.GetPlant();
    this.GetSendingPlant();
    this.GetCounterType();
    this.GetProjectName();
    this.GetProjectNameDropdown();
    
  },
  methods: {
    onCreated(editor) {
        this.editor = Object.seal(editor) // 一定要用 Object.seal() ，否则会报错
    },
    GetSeriesCommon(){
        let seriesDemand=0;
        this.http.post('/api/LTB_Project/getSeriesCommon?spn='+this.spn+"&ProjectID="+this.projectId+"&searchMaterial="+this.material,"", 'loading....')
        .then((result) => {
            console.log("getSeriesCommon",result);
            result.forEach(s => {
              //处理时间格式
              s.onStockFrom=this.transformDate(s.onStockFrom)=="1900-01-01"?"":this.transformDate(s.onStockFrom);
              s.onStockUntil=this.transformDate(s.onStockUntil)=="1900-01-01"?"":this.transformDate(s.onStockUntil);
              //coverageType
              if(s.coverageType==1){
                  s.coverageType="R";
              }
              else if(s.coverageType==2){
                  s.coverageType="Z";
              }
              //counterType
              if(s.counterType==1){
                  s.counterType="direct delivery";
              }
              else if(s.counterType==2){
                  s.counterType="indirect delivery";
              }
              else if(s.counterType==3){
                  s.counterType="manufacturing order supplier";
              }
              else if(s.counterType==4){
                  s.counterType="consumption BU";
              }
              else if(s.counterType==5){
                  s.counterType="flat-rate allocation";
              }
              else if(s.counterType==6){
                  s.counterType="stock transfer";
              }
            });
          this.seriesCommonData = result;
         });

    },
    GetProject() {
        this.http.post('/api/LTB_Project/ssGetProject?spn='+this.spn+"&ProjectID="+this.projectId,"", 'loading....')
        .then((result) => {
          //console.log("GetProject",result,"传来的spn是：",this.spn);
          //this.tableData=result;
          this.pBL=result[0].bl;
          this.pPDN=result[0].pdn;
          this.pSPN=result[0].spn;
          this.pDescription=result[0].obsoleteMaterialDescription;
          this.pStockupstartdate=this.transformDate(result[0].stockUpStartdate);
          //this.pPrice=result[0].price;
          this.totalSeriesDemand=this.moneyFormat(result[0].totalSeriesDemand,0,""); //result[0].totalSeriesDemand;
          //this.totalSeriesCosts=result[0].totalSeriesCosts;
          let priceTypeID=result[0].priceTypeID;
          if(priceTypeID==1 || priceTypeID=="1"){
              this.pPrice=result[0].price+" ￥";
              this.totalSeriesCosts=this.moneyFormat(result[0].totalSeriesCosts,2," ￥"); // result[0].totalSeriesCosts+" ￥";
          }
          else if(priceTypeID==2 || priceTypeID=="2"){
              this.pPrice=result[0].price+" $";
              this.totalSeriesCosts=this.moneyFormat(result[0].totalSeriesCosts,2," $"); //result[0].totalSeriesCosts+" $";
          }
          else if(priceTypeID==3 || priceTypeID=="3"){
              this.pPrice=result[0].price+" €";
              this.totalSeriesCosts=this.moneyFormat(result[0].totalSeriesCosts,2," €"); //result[0].totalSeriesCosts+" €";
          }

          this.GetSeriesCommon();
        });
    },
    GetPlant(){
        this.http.post('/api/LTB_Project/getPlant',"", 'loading....')
        .then((result) => {
            //console.log("getPlant",result);
            this.plantOptions=result;
        });
    },
    GetSendingPlant(){
        this.http.post('/api/LTB_Project/getSendingPlant',"", 'loading....')
        .then((result) => {
            //console.log("getSendingPlant",result);
            this.sendingPlantOptions=result;
        });
    },
    GetCounterType(){
        this.http.post('/api/LTB_Project/getCounterType',"", 'loading....')
        .then((result) => {
            //console.log("getCounterType",result);
            this.counterTypeOptions=result;
        });
    },
    GetProjectName(){
        this.http.post('/api/LTB_Project/getProjectName',"", 'loading....')
        .then((result) => {
            //console.log("getProjectName",result);
            //this.projectNamesOptions=result;
            this.parentMsg=result;
        });
    },
    //第二版 单选下拉的projectName
    GetProjectNameDropdown(){
        this.http.post('/api/LTB_Project/getProjectNameDropdown',"", 'loading....')
        .then((result) => {
            console.log("getProjectNameDropdown",result);
            this.projectNameDropdownOptions=result;
        });
    },     
    BackGoProject(){
        this.$router.push({ 
           name:'LTB_Project',
        }); 
        
        //this.$router.back();  //vue页面缓存
    },
    //双击行变成可编辑的样式
    RowDblclick(row){
        console.log("双击行的数据是:", row);
        row.isEdit=false; 
        this.selectedProject=row.projectNames;
        this.selectedProjectID=row.projectName;
    },
    gethandleChangeData(val){
        this.changedSelectedProject=val;
        console.log("子组件传来改变后的project:", this.changedSelectedProject);
        // //若同时包含HP2,HP4或都不包含，不用弹框提示； 若这俩个只选中了一个，那就弹框提示
        // let number=val.split(';');
        // let hasHP2=number.includes('1');
        // let hasHP4=number.includes('2');
        // let hasLB2=number.includes('35');
        // let hasLB4=number.includes('36');
        // console.log("弹框提示:", number,"hasHP2:",hasHP2,"hasHP2:",hasHP4,"hasHP4:",hasLB2,"hasLB4:",hasLB4);
        // if(hasHP2!=hasHP4){
        //     //console.log("弹框提示一下");
        //     this.$message('弹框提示一下');
        // }
        // if(hasLB2!=hasLB4){
        //     //console.log("弹框提示一下");
        //     this.$message('弹框提示一下');
        // }
    },
    AddSeriesDemands(){
        //1：弹出框添加
        // this.dialogFormVisible = true;
        // //清空数据
        // this.form.counterMaterial="";
        // this.form.counterMaterialDescription="";
        // this.form.plant="";
        // this.form.sendingPlant="";
        // this.form.customer="";
        // this.form.counterType="";
        // this.form.coverageType="";
        // this.form.onStockFrom="";
        // this.form.onStockUntil="";
        // this.form.projectName="";
        // this.form.planRestOfCurrentFY="";
        // this.form.planFirstFollowingFY="";
        // this.form.planSecondFollowingFY="";
        // this.form.planThirdFollowingFY="";
        // this.form.sumPlan="";
        // this.form.componentQuantity="";
        // this.form.totalDemand="";
        // this.form.ssCommonID="";

        //2：table多加一行空数据  留作行内编辑使用
        console.log("addSeriesCommon",this.projectId);
        this.http.post('/api/LTB_Project/addSeriesCommon?ProjectID='+this.projectId,'', 'loading....')
        .then((result) => {
          this.GetProject(); //刷新页面
        });

    },
    UpdateSeriesDemands(index, row) {
        console.log("行内编辑选中行的数据：",index, row);
        //1：弹出框编辑框时
        // this.dialogFormVisible = true;
        // //反填数据  
        // this.form.id=row.id;
        // this.form.projectId=row.projectId;
        // this.form.counterMaterial=row.counterMaterial;
        // this.form.counterMaterialDescription=row.counterMaterialDescription;
        // this.form.plant=row.plant;
        // this.form.sendingPlant=row.sendingPlant;
        // this.form.customer=row.customer;
        // this.form.counterType=row.counterType;
        // this.form.coverageType=row.coverageType;
        // this.form.onStockFrom=row.onStockFrom;
        // this.form.onStockUntil=row.onStockUntil;
        // this.form.projectName=row.projectName;
        // this.form.planRestOfCurrentFY=row.planRestOfCurrentFY;
        // this.form.planFirstFollowingFY=row.planFirstFollowingFY;
        // this.form.planSecondFollowingFY=row.planSecondFollowingFY;
        // this.form.planThirdFollowingFY=row.planThirdFollowingFY;
        // this.form.sumPlan=row.sumPlan;
        // this.form.componentQuantity=row.componentQuantity;
        // this.form.totalDemand=row.totalDemand; 
        // this.form.ssCommonID=row.ssCommonID;

        //2：行编辑
        row.onStockFrom=this.transformDate(row.onStockFrom);
        row.onStockUntil=this.transformDate(row.onStockUntil);
        row.projectName=this.changedSelectedProject;
        //sumPlan和totalDemand挪到后台计算
        // let rest=parseInt(row.planRestOfCurrentFY == "" ? 0: row.planRestOfCurrentFY);
        // let frist=parseInt(row.planFirstFollowingFY == "" ? 0: row.planFirstFollowingFY);
        // let second=parseInt(row.planSecondFollowingFY == "" ? 0: row.planSecondFollowingFY);
        // let third=parseInt(row.planThirdFollowingFY == "" ? 0: row.planThirdFollowingFY);
        // let quantity=parseInt(row.componentQuantity == "" ? 0: row.componentQuantity);
        // let sum = rest+frist+second+third;
        // let total = sum * quantity;
        // row.sumPlan=sum;
        // row.totalDemand=total;
        console.log("处理后的行内编辑的数据：",index, row);
        let addData = JSON.stringify(row);
        console.log("要编辑的数据转成json字符串：",addData);

        //  this.http.post('/api/LTB_Project/addOrUpdateSeriesCommon',row, 'loading....')
        // .then((result) => {
        //   this.GetProject(); //刷新页面
        // });

         this.http.post('/api/LTB_Project/addOrUpdateSeriesCommon?seriesInfo='+addData,'', 'loading....')
        .then((result) => {
          this.GetProject(); //刷新页面
        });

    },
    AddOrUpdateSeriesDemands(){
        this.form.projectId=this.projectId;
        this.form.sumPlan=this.realtimeSumPlan;
        this.form.totalDemand=this.realtimeTotalDemand;
        this.form.onStockFrom=this.transformDate(this.form.onStockFrom);
        this.form.onStockUntil=this.transformDate(this.form.onStockUntil);
        console.log("AddOrUpdateSeriesDemands提交的数据：",this.form);

         this.http.post('/api/LTB_Project/addOrUpdateSeriesCommon',this.form, 'loading....')
        .then((result) => {
          //this.dialogFormVisible=false;
          this.GetProject(); //刷新页面
        });
    },
    DeleteSeriesDemands(index, row){
        console.log("要删除的行数据：",index, row);
        // this.http.post('/api/LTB_Project/deleteSeriesCommon?ID='+row.id+'&ProjectID='+this.projectId,'', 'loading....')
        // .then((result) => {
        //   this.GetProject(); //刷新页面
        // });

        this.$confirm('Are you sure you want to delete it ?', '', {
          confirmButtonText: 'Conform',
          cancelButtonText: 'Cancle',
          type: 'warning'
        }).then(() => {
          this.http.post('/api/LTB_Project/deleteSeriesCommon?ID='+row.id+'&ProjectID='+this.projectId,'', 'loading....')
          .then((result) => {
              this.GetProject(); //刷新页面
              this.$message({
                type: 'success',
                message: 'Successfully deleted !'
              });
          });
        }).catch(() => {
          this.$message({
            type: 'info',
            message: 'Cancelled deletion'
          });          
        });
    },
    ChangeProjectState(){
        this.http.post('/api/LTB_Project/changeProjectState?ProjectID='+this.projectId+'&State='+'Series Done','', 'loading....')
        .then((result) => {
          console.log("Series Done传来的值：",result);
          this.$message(result);
        });
    },
    //富文本编辑器
    //添加Materia和description counter Materia的弹框
    MaterialDialog(){
        //弹出框编辑框
        this.dialogMaterial = true;
    },
    DialogAddMaterial(val){
        val=val.replace("&nbsp;","").replace("&nbsp;","").replace("&nbsp;","").replace("&nbsp;","").replace("<br>","").replace("<br>","").replace("<br>","").replace("<br>","");
        console.log("富文本编辑器的值：",val);
        this.dialogMaterial = false;
        this.http.post('/api/LTB_Project/dialogAddMaterial?MaterialTextarea='+val+"&ProjectID="+this.projectId,'', 'loading....')
        .then((result) => {
           this.GetProject(); //刷新页面
        });

    },
  
  
    //替换表格时间格式 ==> add和update时 时间会变成标准时间格式 还得转化
    formatDate(row, column) {
        // 获取单元格数据
        let date = row[column.property];
        //console.log(date);
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
     //自动获取财年
     AutoFY(){
        let dt = new Date();
        // let restYear=dt.getFullYear()-1;
        // let nowYear=dt.getFullYear();
        // let firstYear=dt.getFullYear()+1;
        // let secandYear=dt.getFullYear()+2;
        // let thirdYear=dt.getFullYear()+3;
        
        let nowYear=dt.getFullYear();
        let nowMonth=dt.getMonth();
        let year=0;
        if(nowMonth<10){
            year=nowYear;
        }
        else if(nowMonth>=10){
            year=nowYear+1;
        }

        let restYear=year-1;
        let firstYear=year+1;
        let secandYear=year+2;
        //let thirdYear=year+3;
        
        this.restFY=restYear+" / "+year;
        this.firstFY=year+" / "+firstYear;
        this.secondFY=firstYear+" / "+secandYear;
        this.thirdFY=secandYear+" / ~";  // +thirdYear;
     },
      /**
      * 将数字转换为金额显示，每三位逗号隔开
      * @method moneyFormat
      * @param {Number} money 数字
      * @param {Number} decimal 小数位
      * @param {string} symbol 金额前缀，如￥或$
      */
    moneyFormat(money, decimal, symbol) {
	    if (!money || isNaN(money)) return "";
          var num = parseFloat(money);
          //console.log("金额每三位逗号隔开",num);
    	  num = String(num.toFixed(decimal ? decimal : 0));
	      var re = /(-?\d+)(\d{3})/;
	      while (re.test(num)) {
		        num = num.replace(re, "$1,$2");
	      }
	    return symbol ? num + symbol: num;
    },

  },
  computed:{
      //实时计算四个FollowingFY的和 ==> this.form.sumPlan
      realtimeSumPlan:function(){
          let rest=parseInt(this.form.planRestOfCurrentFY == "" ? 0: this.form.planRestOfCurrentFY);
          let frist=parseInt(this.form.planFirstFollowingFY == "" ? 0: this.form.planFirstFollowingFY);
          let second=parseInt(this.form.planSecondFollowingFY == "" ? 0: this.form.planSecondFollowingFY);
          let third=parseInt(this.form.planThirdFollowingFY == "" ? 0: this.form.planThirdFollowingFY);
          let sum = rest+frist+second+third;
          return sum.toString();
      },
      //实时计算 realtimeSumPlan*componentQuantity ==> this.form.totalDemand
      realtimeTotalDemand:function(){
          let total = this.realtimeSumPlan * parseInt(this.form.componentQuantity == "" ? 0: this.form.componentQuantity);
          return total.toString();
      },
      //实时计算 totalSeriesDemand=sum(totalDemand) 放在显示哪里计算就行
    //   totalSeriesDemand:function(){
    //       let totsl=0;
    //       this.seriesCommonData.forEach(s => {
    //           totsl+=parseInt(s.totalDemand == "" ? 0: s.totalDemand);
    //       });

    //   },

  },

  // 监听,当路由发生变化的时候执行
  watch:{
    $route(to,from){
      //console.log(to.path);
      //params传参
      this.spn=this.$route.params.spn; 
      this.pdn=this.$route.params.pdn; 
      this.projectId=this.$route.params.projectId; 
      console.log("params传参",this.spn,this.pdn,this.projectId);
      this.AutoFY();
      this.GetProject();
    }
  },
 

  }

</script>

