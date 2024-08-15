<script setup>
import { Recipe } from '@/models/Recipe.js';

const props = defineProps({recipeProp: { type: Recipe, required: true}})
</script>


<template>
<div class="">
    <div>
        <p>{{ recipeProp.category }}</p>
        <p><i class="mdi mdi-heart-outline"></i></p>
    </div>
    <div>
        <p>{{ recipeProp.title }}</p>
        <p>{{ recipeProp.subTitle }}</p>
    </div>
</div>
</template>


<style lang="scss" scoped>

</style>