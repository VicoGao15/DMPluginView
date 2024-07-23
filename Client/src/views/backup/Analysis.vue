<template>
	<div>
		<!-- Table & Timeline -->
		<a-row :gutter="24" type="flex" align="stretch">
			<!-- Table -->
			<a-col :span="24" :lg="24" class="mb-24">
				<CardBackupAnalysis></CardBackupAnalysis>
			</a-col>
			<!-- / Table -->

		</a-row>
		<!-- / Table & Timeline -->

		<!-- Cards -->
		<a-row :gutter="24" type="flex" align="stretch">
			<a-col :span="24" :xl="14" class="mb-24">
			</a-col>
			<a-col :span="24" :xl="10" class="mb-24">
			</a-col>
		</a-row>
		<!-- / Cards -->
	</div>
</template>

<script>

	// Bar chart for "Active Users" card.
	import WidgetCounter from '../../components/Widgets/WidgetCounter' ;
	import CardBackupAnalysis from '../../components/Cards/SdxPlugin/CardBackupAnalysis' ;

	export default ({
		components: {
			WidgetCounter,
			CardBackupAnalysis,
		},
		data() {

			return {
			}
		},

	})

</script>

<style lang="scss">
</style>