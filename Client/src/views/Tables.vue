<!-- 
	This is the tables page, it uses the dashboard layout in: 
	"./layouts/Dashboard.vue" .
 -->

<template>
	<div>
		<a-row type="flex" :gutter="24">

			<!-- Billing Information Column -->
			<a-col :span="24" :md="12" class="mb-24">
				<CardJobCategory></CardJobCategory>
			</a-col>
			<!-- Billing Information Column -->
				
			<!-- Billing Information Column -->
			<a-col :span="24" :md="12" class="mb-24">
				<CardComponentCategory></CardComponentCategory>
			</a-col>
			<!-- Billing Information Column -->

		</a-row>
		<PluginDetailDrawer></PluginDetailDrawer>


	</div>
</template>

<script>

	import CardJobCategory from "../components/Cards/CardJobCategory.vue"
	import CardComponentCategory from "../components/Cards/CardComponentCategory.vue"
	import PluginDetailDrawer from '../components/Drawer/PluginDetailDrawer.vue'

	export default ({
		components: {
			CardJobCategory,
			CardComponentCategory,
			PluginDetailDrawer
		},
		data() {
			return {
				// Associating "Authors" table data with its corresponding property.
				//table1Data: table1Data,

				// Associating "Authors" table columns with its corresponding property.
				//table1Columns: table1Columns,

				// Associating "Projects" table data with its corresponding property.
				//table2Data: table2Data,

				// Associating "Projects" table columns with its corresponding property.
				//table2Columns: table2Columns,
			}
		},
	})

</script>

<style lang="scss">
</style>