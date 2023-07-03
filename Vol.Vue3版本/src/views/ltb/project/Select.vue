<template>
<div class="block" id="selectProject">
  <!-- <div>父组件传过来的值为：{{ projectInfo }}</div> -->
    <el-cascader
    v-model="value"
    :options="projectInfo"
    :props="{ multiple: true, expandTrigger: 'hover' }"
    :show-all-levels="false"
    ref="projectSearch"
    @change="handleChange"
    @blur="prompt()"
    clearable>
    </el-cascader>
  </div>
</template>


<script>
import {useRouter,useRoute} from 'vue-router'

export default {
  name: 'selectProject',
  //接受父组件传来的值
  props:{
      projectInfo:{type:Array},
      selected:{type:String}, 
      selectedID:{type:String}, 
  },
  data() {
    return{
      value:[], //被选中的值
      lookvalue:[], //必须由这个值赋值给value，不然无法回显
      forParentValue:'', //选完project后传给父组件的值
      project:'', 
      chaiselected:'', //拆开拼好后的值 例：P81A/B --> P81A;P81B
    }
  },
  mounted() {
    console.log("父组件传来的值",this.projectInfo,this.selected,this.selectedID);
    this.forParentValue=this.selectedID; //先将父组件传来的值赋给forParentValue，若下拉内容有改变，下面有方法去一起去改变forParentValue
    console.log("要传给父组件的值forParentValue：",this.forParentValue);
    //先把值传给父组件，若下拉有变动handleChange方法里会重新把新值传给父组件；
    //否则会将上次别的material的project传给这个material
    this.$emit('handleChange',this.forParentValue); 
    
    //将拼起来的值拆开 例：P81A/B --> P81A;P81B
    // if(this.selected!=null){
    //   this.chaiSelected(this.selected);
    // }
    // //var chaivalue = this.chaiSelected(this.selected);
    // //console.log("将拼起来的值拆开111：",chaivalue);
    // console.log("将拼起来的值拆开222：",this.chaiselected);

    this.value = this.huixian(this.selectedID);  //用父组件传来的Project的ID来获取反选的值
    //this.value = this.huixian(this.selected); //用父组件传来的Project的Name来获取反选的值
    //this.value = this.huixian(this.chaiselected);  //this.huixian(chaivalue);
    //console.log("value:",this.value);
  },
  methods: {
    chaiSelected(selected){
      let selectedLabel=selected.split(';');
      selectedLabel.forEach(label => {
        if(label=="P81A/B"){
          this.chaiselected=this.chaiselected+"P81A;P81B;";
        }
        else if(label=="Denali LP1/LP2"){
          this.chaiselected=this.chaiselected+"Denali LP1;Denali LP2;";
        }
        else if(label=="P82A/B/C"){
          this.chaiselected=this.chaiselected+"P82A/B;P82C;";
        }
        else if(label=="P60A/B"){
          this.chaiselected=this.chaiselected+"P60A;P60B;";
        }
        else if(label=="T2/6/16"){
          this.chaiselected=this.chaiselected+"T2;T6;T16";
        }
        else if(label=="T2/6"){
          this.chaiselected=this.chaiselected+"T2;T6;";
        }
        else  if(label=="T2/16"){
          this.chaiselected=this.chaiselected+"T2;T16;";
        }
        else  if(label=="T6/16"){
          this.chaiselected=this.chaiselected+"T6;T16;";
        }
        else  if(label=="P68A/B/G"){
          this.chaiselected=this.chaiselected+"P68A;P68B;P68G";
        }
        else  if(label=="P68A/B"){
          this.chaiselected=this.chaiselected+"P68A;P68B;";
        }
        else  if(label=="P68A/G"){
          this.chaiselected=this.chaiselected+"P68A;P68G;";
        }
        else  if(label=="P68B/G"){
          this.chaiselected=this.chaiselected+"P68B;P68G;";
        }
        else  if(label=="P15A/P/F/G"){
          this.chaiselected=this.chaiselected+"P15A;P15P;P15F;P15G;";
        }
        else  if(label=="P15A/P/F"){
          this.chaiselected=this.chaiselected+"P15A;P15P;P15F;";
        }
        else  if(label=="P15A/P/G"){
          this.chaiselected=this.chaiselected+"P15A;P15P;P15G;";
        }
        else  if(label=="P15A/F/G"){
          this.chaiselected=this.chaiselected+"P15A;P15F;P15G;";
        }
        else  if(label=="P15P/F/G"){
          this.chaiselected=this.chaiselected+"P15P;P15F;P15G;";
        }
        else  if(label=="P15A/P"){
          this.chaiselected=this.chaiselected+"P15A;P15P;";
        }
        else  if(label=="P15A/F"){
          this.chaiselected=this.chaiselected+"P15A;P15F;";
        }
        else  if(label=="P15A/G"){
          this.chaiselected=this.chaiselected+"P15A;P15G;";
        }
        else  if(label=="P15P/F"){
          this.chaiselected=this.chaiselected+"P15P;P15F;";
        }
        else  if(label=="P15P/G"){
          this.chaiselected=this.chaiselected+"P15P;P15G;";
        }
        else  if(label=="P15F/G"){
          this.chaiselected=this.chaiselected+"P15F;P15G;";
        }
        else  if(label=="P15S/T"){
          this.chaiselected=this.chaiselected+"P15S;P15T;";
        }
        else {
          this.chaiselected=this.chaiselected+label+";";
        }
      });
      return this.chaiselected;

    },
    huixian(selected){
      //let vlaue=[];
      if(selected!="" && selected!=null){
        let selectedLabel=selected.split(';');
        selectedLabel.forEach(label => {
          //this.lookvalue.push(this.getParentsById(this.projectInfo,label));
          //将string类型的label转为int类型，用ID进行比较
          let newlabel=parseInt(label);
          this.lookvalue.push(this.getParentsById222(this.projectInfo,newlabel));
        });
        //console.log("反选被选中的值",this.lookvalue);
      }
      return this.lookvalue;
    },
    //用父组件传来的Project的ID来获取反选的值
    getParentsById222(list, newlabel) {
      for (let i in list) {
        if (list[i].value == newlabel) {
            //查询到就返回该数组对象的value
             return [list[i].value]
        }
        if (list[i].children) {
            let node = this.getParentsById222(list[i].children, newlabel)
            if (node !== undefined) {
                //查询到把父节把父节点加到数组前面
                node.unshift(list[i].value)
                return node
            }
        }
      }
    },
    //用父组件传来的Project的Name来获取反选的值
    getParentsById(list, label) {
      for (let i in list) {
        if (list[i].label == label) {
            //查询到就返回该数组对象的value
             return [list[i].value]
        }
        if (list[i].children) {
            let node = this.getParentsById(list[i].children, label)
            if (node !== undefined) {
                //查询到把父节把父节点加到数组前面
                node.unshift(list[i].value)
                return node
            }
        }
      }
    },

    handleChange(value) {
      //console.log("重新选值",value);
      this.project=''; //须得清空 不然最后的值是累加的
      this.forParentValue='';
      if(value.length!=0){
        value.forEach(v => {
          this.project += v[1]+';'; //v[1]才是project的id
        });
      }
      //console.log("重新选择的project是：",this.project);
      this.forParentValue=this.project.substring(0,this.project.length-1);
      console.log("要传给父组件的project是：",this.forParentValue);
      this.$emit('handleChange',this.forParentValue); //把值传给父组件
    },

    prompt(){
      //this.$refs.projectSearch.value;
      // setTimeout(()=>{
      //   console.log("失去焦点1时的值是：",this.value);
      //   console.log("失去焦点2时的值是：",this.$refs.projectSearch.value);
      // },100)
      
    },
    
  }
  
  }

</script>

